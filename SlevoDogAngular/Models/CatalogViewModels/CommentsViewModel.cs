using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.CatalogViewModels
{
    public class CommentsViewModel
    {
        /// <summary>
        /// Unique identification of Comment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of author of comment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date Insert of sale
        /// </summary>
        public string DateInsert { get; set; }

        /// <summary>
        /// Rank of comment
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Text of comment
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Foreign key for parrent comment if exist
        /// </summary>
        public int? FkParrentComment { get; set; }

        /// <summary>
        /// Collection of comments
        /// </summary>
        public CommentsCollection commentsCollection { get; } = new CommentsCollection();

    }
}
