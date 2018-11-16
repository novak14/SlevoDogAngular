using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using SlevoDogAngular.Models.CatalogViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Catalog.Dal.Entities;
using Shared.Dal.Entities;

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
            var currentUser = HttpContext.User;

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
        public async Task<SaleViewModel> CategoryItemsAsync(int categoryId, string sortOrder)
        {
            SaleViewModel sale = new SaleViewModel();

            var test = await _catalogService.GetCategoryItems(categoryId, sortOrder);

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

        [HttpGet("[action]")]
        public async Task<List<Category>> GetCategories()
        {
            var categories = await _catalogService.GetCategories();
            return categories;
        }

        [HttpGet("[action]")]
        public async Task<SaleViewModel> GetSaleByKeyWord(string keyword)
        {
            SaleViewModel saleCollection = new SaleViewModel();

            var suggestSaleCollection = await _catalogService.GetSalesSuggest(keyword);

            foreach (var item in suggestSaleCollection)
            {
                SaleViewModel saleItem = new SaleViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    PriceAfterSale = item.PriceAfterSale.ToString("C0"),
                    Image = item.Image,
                    LinkFirm = item.LinkFirm,
                };

                saleCollection.saleCollection.collections.Add(saleItem);
            }
            return saleCollection;
        }
    }
}
