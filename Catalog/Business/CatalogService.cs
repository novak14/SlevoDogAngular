using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using MlkPwgen;
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

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _saleRepository.GetCategories();
            return categories;
        }

        public async Task<Sale> LoadByIdAsync(int id)
        {
            var test = await _saleRepository.LoadByIdAsync(id);
            return test;
        }

        public async Task AddRankForSale(int saleId, int rank, int userId)
        {
            await _saleRepository.AddRank(saleId, rank);
            await _saleRepository.ConnectUserRank(saleId, userId);
        }

        public async Task<int> CheckRankSaleUser(int saleId, int userId)
        {
            return await _saleRepository.CheckRankUser(saleId, userId);
        }

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

        public async Task AddRankForComment(int commentId, int rank, int userId)
        {
            await _commentRepository.AddRank(commentId, rank);
            await _commentRepository.ConnectUserRank(commentId, userId);
        }

        public async Task<int> CheckRankUser(int commentId, int userId)
        {
            return await _commentRepository.CheckRankUser(commentId, userId);
        }

        public async Task<User> GetUserByOriginalId(string uniqueString)
        {
            return await _userRepository.GetUserByUniqueString(uniqueString);
        }

        public async Task InsertUser(string username, string email, string id)
        {
            await _userRepository.InsertUserAsync(username, email, id);
        }

        public async Task<List<Comments>> GetCommentsAsync(int saleId)
        {
            var comments = await _commentRepository.GetCommentsAsync(saleId);
            return comments;
        }
    }
}
