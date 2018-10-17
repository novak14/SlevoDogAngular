﻿using SlevoDogAngular.Models.CatalogViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.CatalogViewModels
{
    public class SaleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public string PriceAfterSale { get; set; }
        public decimal AveragePrice { get; set; }

        [DataType(DataType.Currency)]
        public string OriginPrice { get; set; }
        public string Image { get; set; }
        public string DateInsert { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string LinkFirm { get; set; }
        public string Description { get; set; }

        public int PercentSale { get; set; }
        public int RankSale { get; set; }
        public int FkShop { get; set; }

        // Category
        public string CategoryName { get; set; }
        public int CategoryId { get; set;}

        public SaleCollection saleCollection { get; } = new SaleCollection();

        public string IdUser { get; set; }
    }
}
