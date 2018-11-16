using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Category
    {
        /// <summary>
        /// Unique identification of Category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Check if Category is disabled and if can be shown on website
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Foreign key for parent cateogry
        /// </summary>
        public int FkParentCategory { get; set; }

        /// <summary>
        /// Navigation property for Sale
        /// </summary>
        public Sale Sale { get; set; }
    }
}
