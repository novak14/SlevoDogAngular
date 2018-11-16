using SlevoDogAngular.Models.CatalogViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.CatalogViewModels
{
    public class SaleViewModel
    {
        /// <summary>
        /// Unique identification of object
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of sale
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of sale after sale
        /// </summary>
        [DataType(DataType.Currency)]
        public string PriceAfterSale { get; set; }

        /// <summary>
        /// Average price of sale
        /// </summary>
        public decimal AveragePrice { get; set; }

        /// <summary>
        /// Origin price of sale
        /// </summary>
        [DataType(DataType.Currency)]
        public string OriginPrice { get; set; }

        /// <summary>
        /// Percentage sale of sale
        /// </summary>
        public int PercentSale { get; set; }

        /// <summary>
        /// Image of sale
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Date Insert of sale
        /// </summary>
        public string DateInsert { get; set; }

        /// <summary>
        /// From DateTime when is sale valid
        /// </summary>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// To DateTime is valid sale
        /// </summary>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// For creating link to company(firm)
        /// </summary>
        public string LinkFirm { get; set; }

        /// <summary>
        /// Description of sale
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Overall rank of the sale
        /// </summary>
        public int RankSale { get; set; }

        /// <summary>
        /// Foreign key to shop
        /// </summary>
        public int FkShop { get; set; }

        // Category
        /// <summary>
        /// Name of category where is the sale
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Unique identification of category where is the sale
        /// </summary>
        public int CategoryId { get; set;}

        /// <summary>
        /// Collection of sale
        /// </summary>
        public SaleCollection saleCollection { get; } = new SaleCollection();

        /// <summary>
        /// Unique identification of user
        /// </summary>
        public string IdUser { get; set; }
    }
}
