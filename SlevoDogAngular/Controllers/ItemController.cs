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

namespace SlevoDogAngular.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly CatalogService _catalogService;

        public ItemController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet("[action]")]
        public async Task<SaleViewModel> ItemAsync(int? id)
        {
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

            foreach(var item in test1.Comments)
            {
                CommentsViewModel commentsViewModel = new CommentsViewModel
                {
                    Name = item.Name,
                    Text = item.Text,
                    DateInsert = item.DateInsert,
                    Rank = item.Rank
                };
                saleItem.Comments.Add(commentsViewModel);
            }

            return saleItem;
        }

        [HttpPost("[action]")]
        public JsonResult AddComments([FromBody]CommentsViewModel model)
        {
            string cookie = _catalogService.InsertComment(model.Name, model.Text, model.Id);
            return Json(cookie);
        }

        [HttpGet("[action]")]
        public JsonResult GetUserNameComment(string cookie)
        {
            var checkUser = _catalogService.CheckUserCookie(cookie);
            return Json(checkUser.UserName);
        }

        [HttpGet("[action]")]
        public List<CommentsViewModel> GetComments(int saleId)
        {
            var comments = _catalogService.GetComments(saleId);
            List<CommentsViewModel> commentsMap = Mapper.Map<List<Comments>, List<CommentsViewModel>>(comments);

            return commentsMap;
        }
    }
}
