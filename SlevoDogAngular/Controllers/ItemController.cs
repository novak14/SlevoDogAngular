using Catalog.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SlevoDogAngular.Models.CatalogViewModels;
using SlevoDogAngular.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Dal.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SlevoDogAngular.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ItemController(CatalogService catalogService,
            UserManager<ApplicationUser> userManager)
        {
            _catalogService = catalogService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<SaleViewModel> ItemAsync(int? id)
        {
            var user = HttpContext.User;
            var test = _userManager.GetUserAsync(User);
            var testId = _userManager.GetUserId(User);
            
            var test1 = id != null ? await _catalogService.LoadByIdAsync(id.Value) : throw new Exception(nameof(id));

            SaleViewModel saleItem = new SaleViewModel
            {
                Id = test1.Id,
                Name = test1.Name,
                PriceAfterSale = test1.PriceAfterSale.ToString("C0"),
                OriginPrice = test1.OriginPrice.ToString("C0"),
                Image = test1.Image,
                ValidFrom = test1.ValidFrom,
                ValidTo = test1.ValidTo,
                LinkFirm = test1.LinkFirm,
                Description = test1.Description,
                PercentSale = (int)test1.PercentSale,
                DateInsert = TimeAgo(test1.DateInsert),
                RankSale = test1.RankSale,
                CategoryName = test1.Category?.Name ?? "Ostatní",
                CategoryId = test1.Category?.Id ?? 5
            };

            return saleItem;
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> RankSale([FromBody]SaleViewModel saleViewModel)
        {
            var originalUser = await ExistUser();
            if (originalUser == null)
                return BadRequest();

            if (saleViewModel.Id > 0 && saleViewModel.RankSale > 0)
            {
                var user = _catalogService.GetUserByOriginalId(originalUser.Id);

                var amountRanks = await _catalogService.CheckRankSaleUser(saleViewModel.Id, user.Id);
                if (amountRanks > 0 ) 
                    return BadRequest();
                    
                await _catalogService.AddRankForSale(saleViewModel.Id, saleViewModel.RankSale, user.Id);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCommentsAsync([FromBody]CommentsViewModel model)
        {
            var originalUser = await ExistUser();
            if (originalUser == null)
                return BadRequest();

            var user = await _catalogService.GetUserByOriginalId(originalUser.Id);
            await _catalogService.InsertCommentAsync(model.Name, model.Text, model.Id, model.FkParrentComment, user.Id);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserNameCommentAsync()
        {
            var originalUser = await ExistUser();
            if (originalUser == null)
                return BadRequest();

            var userName = originalUser.Email.Split('@')[0];
            return Json(userName);
        }

        [HttpGet("[action]")]
        public async Task<List<CommentsViewModel>> GetCommentsAsync(int saleId)
        {
            var comments = await _catalogService.GetCommentsAsync(saleId);
            //List<CommentsViewModel> commentsMap = Mapper.Map<List<Comments>, List<CommentsViewModel>>(comments);
            List<CommentsViewModel> commentsMap = new List<CommentsViewModel>();

            var childs = comments.Where(x => x.Rank > 0 && x.FkParrentComment != null).OrderBy(c => c.Rank).ToList();
            foreach (var childComment in childs)
            {
                var index = comments.FindIndex(a => a.Id == childComment.FkParrentComment);
                var indexChildren = comments.FindIndex(b => b.Id == childComment.Id);
                var changeParrentPosition = comments[index];
                comments.RemoveAt(index);
                comments.Insert(indexChildren, changeParrentPosition);
            }


            foreach (var item in comments)
            {
                CommentsViewModel commentsViewModel = new CommentsViewModel
                {
                    Name = item.Name,
                    Text = item.Text,
                    DateInsert = TimeAgo(item.DateInsert),
                    Rank = item.Rank,
                    FkParrentComment = item.FkParrentComment,
                    Id = item.Id
                };
                commentsMap.Add(commentsViewModel);
            }

            return commentsMap;
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> RankComment([FromBody]CommentsViewModel commentsViewModel)
        {
            var originalUser = await ExistUser();
            if (originalUser == null)
                return BadRequest();

            if (commentsViewModel.Id > 0 && commentsViewModel.Rank > 0)
            {
                var user = await _catalogService.GetUserByOriginalId(originalUser.Id);
                
                var amountRanks = await _catalogService.CheckRankUser(commentsViewModel.Id, user.Id);
                if (amountRanks > 0 ) 
                    return BadRequest();
                    
                await _catalogService.AddRankForComment(commentsViewModel.Id, commentsViewModel.Rank, user.Id);
                return Ok();
            }

            return BadRequest();
        }

        public async Task<ApplicationUser> ExistUser()
        {
            var checkUser = await _userManager.GetUserAsync(User);

            if (checkUser == null)
                return null;
            return checkUser;
        }

        public string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("před {0} {1}",
                years, years == 1 ? "rokem" : "roky");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days - (months * 30) > 15)
                    months += 1;
                return String.Format("před {0} {1}",
                months, months == 1 ? "měsícem" : "měsíci");
            }
            if (span.Days > 0)
                return String.Format("před {0} {1}",
                span.Days, span.Days == 1 ? "dnem" : "dny");
            if (span.Hours > 0)
                return String.Format("před {0} hod",
                span.Hours);
            if (span.Minutes > 0)
                return String.Format("před {0} min",
                span.Minutes);
            if (span.Seconds > 5)
                return String.Format("před {0} sekundami", span.Seconds);
            if (span.Seconds <= 5)
                return "právě teď";
            return string.Empty;
        }
    }
}
