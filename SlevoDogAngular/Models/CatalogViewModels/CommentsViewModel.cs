using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.CatalogViewModels
{
    public class CommentsViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public DateTime DateInsertComment { get; set; }
        public int RankComment { get; set; }
        public string Text { get; set; }
    }
}
