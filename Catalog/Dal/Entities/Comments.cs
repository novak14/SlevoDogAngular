using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public int FkSale { get; set; }
        public DateTime DateInsert { get; set; }
        public string FkUser { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public string Text { get; set; }
        public int? FkParrentComment { get; set; }
        public bool Disabled { get; set; }

        public Sale Sale { get; set; }
    }
}
