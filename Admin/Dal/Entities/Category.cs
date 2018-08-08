using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Dal.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
        public int FkParentCategory { get; set; }

        public bool CheckCategory => false;
    }
}
