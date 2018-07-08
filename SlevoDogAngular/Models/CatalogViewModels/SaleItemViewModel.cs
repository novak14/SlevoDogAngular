using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.CatalogViewModels
{
    public class SaleItemViewModel
    {
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
        public int PercentSale { get; set; }

        // Comments
        public int CommentId { get; set; }
        public int FkSale { get; set; }
        public DateTime CommentDateInsert { get; set; }
        public int FkUser { get; set; }
        public string CommentName { get; set; }
        public int Rank { get; set; }
        public string Text { get; set; }
        public int FkParrentComment { get; set; }
        public bool Disabled { get; set; }
    }
}
