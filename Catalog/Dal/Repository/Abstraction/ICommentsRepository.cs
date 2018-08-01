using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Dal.Entities;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface ICommentsRepository
    {
        List<Comments> GetComments(int saleId);
    }
}
