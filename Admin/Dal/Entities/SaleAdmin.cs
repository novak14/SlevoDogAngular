using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Dal.Entities
{
    public class SaleAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceAfterSale { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal OriginPrice { get; set; }
        public string Image { get; set; }

        public DateTime DateInsert { get; set; }

        private DateTime? _validFrom;
        public DateTime? ValidFrom {
            get => _validFrom;
            set
            {
                if (value <= DateTime.MinValue)
                    _validFrom = null;
                else
                    _validFrom = value;
            }
        }

        private DateTime? _validTo;
        public DateTime? ValidTo {
            get => _validTo;
            set
            {
                if (value <= DateTime.MinValue)
                    _validTo = null;
                else
                    _validTo = value;
            }
        }
        public string LinkFirm { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }
        public int FkShop { get; set; }
        public string NameShop { get; set; }
        public string[] Keywords { get; set; }


        public decimal PercentSale => Math.Round(100 - (PriceAfterSale * 100 / OriginPrice));

        public int[] CheckedCategories { get; set; }


    }
}
