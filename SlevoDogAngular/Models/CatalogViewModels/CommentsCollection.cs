using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.CatalogViewModels
{
    public class CommentsCollection
    {
        public List<CommentsViewModel> collections;
        public CommentsCollection()
        {
            collections = new List<CommentsViewModel>();
        }
    }
}
