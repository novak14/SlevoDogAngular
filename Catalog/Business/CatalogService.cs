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

        public async Task<Sale> LoadByIdAsync(int id)
        {
            var test = await _saleRepository.LoadByIdAsync(id);
            return test;
        }

        public async Task<string> InsertCommentAsync(string AuthorName, string Text, int Id, string IdUser = null)
        {
            Comments comments = new Comments
            {
                DateInsert = DateTime.Now,
                Disabled = false,
                FkSale = Id,
                Name = AuthorName,
                Text = Text,
                Rank = 0,
                FkUser = IdUser
            };
            await _commentRepository.InsertCommentAsync(comments);

            return await InsertUserFromCommentsAsync(AuthorName);
        }

        public async Task<User> CheckUserCookieAsync(string cookie)
        {
            return await _userRepository.GetUserByCookieAsync(cookie);
        }

        public async Task<string> InsertUserFromCommentsAsync(string username)
        {
            string cookie = PasswordGenerator.Generate(length: 15, allowed: Sets.Alphanumerics);
            return await _userRepository.InsertUserAsync(username, cookie);
        }

        public async Task<List<Comments>> GetCommentsAsync(int saleId)
        {
            return await _commentRepository.GetCommentsAsync(saleId);
        }
    }
}
