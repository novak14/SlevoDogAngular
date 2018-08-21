using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface ICommentsRepository
    {
        Task<List<Comments>> GetCommentsAsync(int saleId);
        Task InsertCommentAsync(Comments comments);
        Task AddRank(int commentId, int rank);
    }
}
