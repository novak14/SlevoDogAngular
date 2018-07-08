using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Business.DTOObjects
{
    public class DTOCatalogSaleCollection
    {
        public List<DTOCatalogSale> collections;
        public DTOCatalogSaleCollection()
        {
            collections = new List<DTOCatalogSale>();
        }
    }
}
