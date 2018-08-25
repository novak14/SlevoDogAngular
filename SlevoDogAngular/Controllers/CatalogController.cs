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
        public async Task<SaleViewModel> AllItemsAsync(string sortOrder)
        {
            SaleViewModel sale = new SaleViewModel();

            try
            {
                var test = await _catalogService.LoadSortingAsync(sortOrder);

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
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return sale;
        }

        [HttpGet("[action]")]
        public async Task<SaleViewModel> CategoryItemsAsync(int categoryId)
        {
            SaleViewModel sale = new SaleViewModel();

            var test = await _catalogService.GetCategoryItems(categoryId);

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
    }
}
