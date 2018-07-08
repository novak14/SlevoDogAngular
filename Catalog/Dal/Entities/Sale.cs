using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceAfterSale { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal OriginPrice { get; set; }
        public string Image { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string LinkFirm { get; set; }
        public string Description { get; set; }
        public bool bDisabled { get; set; }
        public decimal PercentSale { get; set; }

        public List<Comments> Comments { get; set; } = new List<Comments>();
    }
}
