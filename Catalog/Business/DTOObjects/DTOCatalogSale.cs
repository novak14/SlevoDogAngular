using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Business.DTOObjects
{
    public class DTOCatalogSale
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceAfterSale { get; set; }
        public decimal OriginPrice { get; set; }
        public string Image { get; set; }
        public string LinkFirm { get; set; }

        public decimal PercentSale
        {
            get { return OriginPrice - (PriceAfterSale * 100 / 120); }
        }

        public DTOCatalogSaleCollection dtoSaleCollection { get; } = new DTOCatalogSaleCollection();
    }
}
