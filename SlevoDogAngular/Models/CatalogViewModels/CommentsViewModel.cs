using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.CatalogViewModels
{
    public class CommentsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateInsert { get; set; }
        public int Rank { get; set; }
        public string Text { get; set; }
        public int? FkParrentComment { get; set; }

        public string Cookie { get; set; }

        public CommentsCollection commentsCollection { get; } = new CommentsCollection();

    }
}
