using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using MlkPwgen;
using Shared.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Business
{
    public class CatalogService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentsRepository _commentRepository;

        public CatalogService(ISaleRepository loadCatalog,
                              IUserRepository userRepository,
                              ICommentsRepository commentRepository)
        {
            _saleRepository = loadCatalog;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// Method which return collection of sales, depends of sortOrder
        /// </summary>
        /// <param name="sortOrder">Says how to sort</param>
        /// <returns>Sale collection</returns>
        public async Task<List<Sale>> LoadSortingAsync(string sortOrder)
        {
            switch (sortOrder)
            {
                case "cheapest":
                    return await _saleRepository.LoadCheapestAsync();
                case "newest":
                    return await _saleRepository.LoadNewestAsync();
                case "sale":
                    return await _saleRepository.LoadBiggestSaleAsync();
                default:
                    return await _saleRepository.LoadAllAsync();
            }
        }

        /// <summary>
        /// Returns collection of sales in the specific category and it depends on sortOrder 
        /// </summary>
        /// <param name="categoryId">Unique identification of category</param>
        /// <param name="sortOrder">Says how to sort</param>
        /// <returns>Sale collection</returns>
        public async Task<List<Sale>> GetCategoryItems(int categoryId, string sortOrder)
        {
            var sorting = await _saleRepository.GetCategoryItems(categoryId);;
            switch (sortOrder)
            {
                case "cheapest":
                    return sorting.OrderBy(a => a.PriceAfterSale).ToList();
                case "newest":
                    return sorting.OrderBy(a => a.DateInsert).ToList();
                case "sale":
                    return sorting.OrderBy(a => a.PercentSale).ToList();
                default:
                   return sorting;
            }
        }

        /// <summary>
        /// Get collection of all categories
        /// </summary>
        /// <returns>Collection of categories</returns>
        public async Task<List<Category>> GetCategories()
        {
            var categories = await _saleRepository.GetCategories();
            return categories;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Sale> LoadByIdAsync(int id)
        {
            var sale = await _saleRepository.LoadByIdAsync(id);
            return sale;
        }

        /// <summary>
        /// Add one point up for sale, relate to ranking
        /// </summary>
        /// <param name="saleId">Unique identification of sale</param>
        /// <param name="rank">How much rank it will update</param>
        /// <param name="userId">Unique identification of user</param>
        /// <returns></returns>
        public async Task AddRankForSale(int saleId, int rank, int userId)
        {
            await _saleRepository.AddRank(saleId, rank);
            await _saleRepository.ConnectUserRank(saleId, userId);
        }

        /// <summary>
        /// Check if user already made rank for specific sale
        /// </summary>
        /// <param name="saleId">Unique identification of sale</param>
        /// <param name="userId">Unique identification of user</param>
        /// <returns>Amount of user ranks by user for specific comment</returns>
        public async Task<int> CheckRankSaleUser(int saleId, int userId)
        {
            return await _saleRepository.CheckRankUser(saleId, userId);
        }

        /// <summary>
        /// Insert comment
        /// </summary>
        /// <param name="AuthorName">Name of comment author</param>
        /// <param name="Text">Text of comment</param>
        /// <param name="Id">Foreign key for sale</param>
        /// <param name="ParentCommentId">Foreign key for parrent comment if exist</param>
        /// <param name="IdUser">Unique identification of user</param>
        /// <returns></returns>
        public async Task InsertCommentAsync(string AuthorName, string Text, int Id, int? ParentCommentId, int IdUser)
        {
            Comments comments = new Comments
            {
                DateInsert = DateTime.Now,
                Disabled = false,
                FkSale = Id,
                Name = AuthorName,
                Text = Text,
                Rank = 0,
                FkUser = IdUser,
                FkParrentComment = ParentCommentId
            };
            await _commentRepository.InsertCommentAsync(comments);
        }

        /// <summary>
        /// Add rank for comment by user
        /// </summary>
        /// <param name="commentId">Unique identification of comment</param>
        /// <param name="rank">How much rank it will update</param>
        /// <param name="userId">Unique identification of user</param>
        /// <returns></returns>
        public async Task AddRankForComment(int commentId, int rank, int userId)
        {
            await _commentRepository.AddRank(commentId, rank);
            await _commentRepository.ConnectUserRank(commentId, userId);
        }

        /// <summary>
        /// Check if user already ranked the specific comment
        /// </summary>
        /// <param name="commentId">Unique identification of comment</param>
        /// <param name="userId">Unique identification of user</param>
        /// <returns>Amount of user ranks by user for specific comment</returns>
        public async Task<int> CheckRankUser(int commentId, int userId)
        {
            return await _commentRepository.CheckRankUser(commentId, userId);
        }

        /// <summary>
        /// Find the user in custom table and return user with number id
        /// </summary>
        /// <param name="uniqueString">Id in aspnetUser table for filtering in custom table</param>
        /// <returns>Custom user with specific id</returns>
        public async Task<User> GetUserByOriginalId(string uniqueString)
        {
            return await _userRepository.GetUserByUniqueString(uniqueString);
        }

        /// <summary>
        /// Insert user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="id">Unique string which is Id of AspNetUser</param>
        /// <returns></returns>
        public async Task InsertUser(string username, string email, string id)
        {
            await _userRepository.InsertUserAsync(username, email, id);
        }

        /// <summary>
        /// Get collection of comments by id of sale
        /// </summary>
        /// <param name="saleId">Unique identification of sale</param>
        /// <returns></returns>
        public async Task<List<Comments>> GetCommentsAsync(int saleId)
        {
            var comments = await _commentRepository.GetCommentsAsync(saleId);
            return comments;
        }

        /// <summary>
        /// Find sales based on keyword, which was inputed by user
        /// </summary>
        /// <param name="keyword">Searching word by customer</param>
        /// <returns>Collection of sales</returns>
        public async Task<List<Sale>> GetSalesSuggest(string keyword)
        {
            return await _saleRepository.GetSalesSuggest(keyword);
        }
    }
}
