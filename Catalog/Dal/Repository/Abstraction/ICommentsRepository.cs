using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface ICommentsRepository
    {
        /// <summary>
        /// Load collection of comments for specific sale
        /// </summary>
        /// <param name="saleId">Unique identification of sale</param>
        /// <returns></returns>
        Task<List<Comments>> GetCommentsAsync(int saleId);

        /// <summary>
        /// Insert comment to table
        /// </summary>
        /// <param name="comments">Object of comment</param>
        /// <returns></returns>
        Task InsertCommentAsync(Comments comments);

        /// <summary>
        /// Add rank for comment
        /// </summary>
        /// <param name="commentId">Unique identification of comment</param>
        /// <param name="rank">New rank for comment</param>
        /// <returns></returns>
        Task AddRank(int commentId, int rank);

        /// <summary>
        /// Check if user already ranked the specific comment
        /// </summary>
        /// <param name="commentId">Unique identification of comment</param>
        /// <param name="userId">Unique identification of user</param>
        /// <returns>Amount of rank for specific comment by the user, needs to check when is called</returns>
        Task<int> CheckRankUser(int commentId, int userId);

        /// <summary>
        /// Insert comment and user to RankCommentsUser
        /// </summary>
        /// <param name="commentId">Unique identification of comment</param>
        /// <param name="userId">Unique identification of user</param>
        /// <returns></returns>
        Task ConnectUserRank(int commentId, int userId);
    }
}
