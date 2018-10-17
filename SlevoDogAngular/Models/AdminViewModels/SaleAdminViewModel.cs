using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.AdminViewModels
{
    public class SaleAdminViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PriceAfterSale { get; set; }

        [DataType(DataType.Currency)]
        public decimal? AveragePrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal OriginPrice { get; set; }
        public string Image { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ValidFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime ValidTo { get; set; }

        [Required]
        public string LinkFirm { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }

        public string NameShop { get; set; }

        public int[] CheckedCategories { get; set; }
    }
}
