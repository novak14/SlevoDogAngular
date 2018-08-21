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

namespace SlevoDogAngular.Controllers
{
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

        [HttpGet("[action]")]
        public async Task<SaleViewModel> ItemAsync(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var check = User.Identity.IsAuthenticated;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var test1 = id != null ? await _catalogService.LoadByIdAsync(id.Value) : throw new Exception(nameof(id));
            stopwatch.Stop();
            var dapper = stopwatch.Elapsed;

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
                DateInsert = test1.DateInsert,
            };

            foreach (var item in test1.Comments)
            {
                CommentsViewModel commentsViewModel = new CommentsViewModel
                {
                    Name = item.Name,
                    Text = item.Text,
                    DateInsert = TimeAgo(item.DateInsert),
                    Rank = item.Rank
                };
                saleItem.Comments.Add(commentsViewModel);
            }

            return saleItem;
        }

        [HttpPost("[action]")]
        public async Task<JsonResult> AddCommentsAsync([FromBody]CommentsViewModel model)
        {
            string cookie = await _catalogService.InsertCommentAsync(model.Name, model.Text, model.Id, model.FkParrentComment);
            return Json(cookie);
        }

        [HttpGet("[action]")]
        public async Task<JsonResult> GetUserNameCommentAsync(string cookie)
        {
            var checkUser = await _catalogService.CheckUserCookieAsync(cookie);
            return Json(checkUser.UserName);
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
            if (commentsViewModel.Id > 0 && commentsViewModel.Rank > 0)
            {
                await _catalogService.AddRankForComment(commentsViewModel.Id, commentsViewModel.Rank);
                return Ok();
            }

            return BadRequest();
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
