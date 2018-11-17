using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Sale
    {
        /// <summary>
        /// Unique identification of object
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of sale
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of sale after sale
        /// </summary>
        public decimal PriceAfterSale { get; set; }

        /// <summary>
        /// Average price of sale
        /// </summary>
        public decimal AveragePrice { get; set; }

        /// <summary>
        /// Origin price of sale
        /// </summary>
        public decimal OriginPrice { get; set; }

        /// <summary>
        /// Percentage sale of sale
        /// </summary>
        public decimal PercentSale { get; set; }

        /// <summary>
        /// Image of sale
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Date Insert of sale
        /// </summary>
        public DateTime DateInsert { get; set; }

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
        /// Check if Sale is disabled and if can be shown on website
        /// </summary>
        public bool bDisabled { get; set; }

        /// <summary>
        /// Overall rank of the sale
        /// </summary>
        public int RankSale { get; set; }

        /// <summary>
        /// Foreign key to shop
        /// </summary>
        public int FkShop { get; set; }

        /// <summary>
        /// Collectioon of comments
        /// </summary>
        public List<Comments> Comments { get; set; } = new List<Comments>();

        /// <summary>
        /// Navigation property to cateogory
        /// </summary>
        public Category Category { get; set; }
    }
}
