using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UniqueString { get; set; }
    }
}
