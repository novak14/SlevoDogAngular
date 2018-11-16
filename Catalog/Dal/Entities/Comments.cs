using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Comments
    {
        /// <summary>
        /// Unique identification of Comment
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to Sale
        /// </summary>
        public int FkSale { get; set; }

        /// <summary>
        /// Date Insert of sale
        /// </summary>
        public DateTime DateInsert { get; set; }

        /// <summary>
        /// Foreign key to user
        /// </summary>
        public int FkUser { get; set; }

        /// <summary>
        /// Author name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Rank of comment
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Text of comment
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Foreign key to parrent comment
        /// </summary>
        public int? FkParrentComment { get; set; }

        /// <summary>
        /// Check if Comment is disabled and if can be shown on website
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Navigation property to Sale
        /// </summary>
        public Sale Sale { get; set; }
    }
}
