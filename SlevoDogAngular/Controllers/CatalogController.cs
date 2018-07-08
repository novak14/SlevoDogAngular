using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using SlevoDogAngular.Models.CatalogViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SlevoDogAngular.Controllers
{
    [Route("api/[controller]")]
    public class CatalogController : Controller
    {
        private readonly CatalogService _catalogService;

        public CatalogController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet("[action]")]
        public SaleViewModel Index(string sortOrder)
        {
            //if (String.IsNullOrEmpty(sortOrder))
            //{
            //    ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            //    ViewData["NewSortParm"] = String.IsNullOrEmpty(sortOrder) ? "new_desc" : "";
            //    ViewData["SaleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "sale_desc" : "";
            //}
            //else
            //{
            //    ViewData["PriceSortParm"] = "price_desc" ;
            //    ViewData["NewSortParm"] =  "new_desc";
            //    ViewData["SaleSortParm"] = "sale_desc";
            //}

            // var test = _catalogService.LoadSorting(sortOrder);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var test = _catalogService.LoadSorting(sortOrder);
            stopwatch.Stop();
            var dapper = stopwatch.Elapsed;


            SaleViewModel sale = new SaleViewModel();

            foreach (var item in test)
            {
                SaleViewModel saleItem = new SaleViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    PriceAfterSale = item.PriceAfterSale.ToString("C0"),
                    OriginPrice = item.OriginPrice.ToString("C0"),
                    Image = item.Image,
                    LinkFirm = item.LinkFirm,
                    PercentSale = (int)item.PercentSale
                };

                sale.saleCollection.collections.Add(saleItem);
            }
            return sale;
        }

        public IActionResult Filter(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            return View("Index");
        }
    }
}
