using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.AdminViewModels
{
    public class SaleAdminViewModel
    {
        /// <summary>
        /// Name of sale
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Price of sale after sale
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal PriceAfterSale { get; set; }

        /// <summary>
        /// Average price of sale
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal? AveragePrice { get; set; }

        /// <summary>
        ///         /// <summary>
        /// Origin price of sale
        /// </summary>
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal OriginPrice { get; set; }

        /// <summary>
        /// Percentage sale of sale
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// From DateTime when is sale valid
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// To DateTime is valid sale
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// For creating link to company(firm)
        /// </summary>
        [Required]
        public string LinkFirm { get; set; }

        /// <summary>
        /// Description of sale
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Check if it will show on web
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Name of shop
        /// </summary>
        public string NameShop { get; set; }

        /// <summary>
        /// Array of keywords
        /// </summary>
        public string[] Keywords { get; set; }

        /// <summary>
        /// Array of ids keyword
        /// </summary>
        public int[] KeywordIds { get; set; }

        /// <summary>
        /// Which category is checked
        /// </summary>
        public int[] CheckedCategories { get; set; }
    }
}
