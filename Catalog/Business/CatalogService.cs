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
        private readonly ISaleRepository _loadCatalog;
        private readonly IUserRepository _userRepository;

        public CatalogService(ISaleRepository loadCatalog,
                              IUserRepository userRepository)
        {
            _loadCatalog = loadCatalog;
            _userRepository = userRepository;
        }

        public List<Sale> LoadAll(string sortOrder)
        {
            var fullCatalog = _loadCatalog.LoadAll();
            switch (sortOrder)
            {
                case "cheapest":
                    fullCatalog = fullCatalog.OrderBy(s => s.PriceAfterSale).ToList();
                    break;
                case "newest":
                    fullCatalog = fullCatalog.OrderByDescending(s => s.DateInsert).ToList();
                    break;
                case "sale":
                    fullCatalog = fullCatalog.OrderByDescending(s => s.PercentSale).ToList();
                    break;
            }
            return fullCatalog.ToList();
        }

        public List<Sale> LoadSorting(string sortOrder)
        {
            switch (sortOrder)
            {
                case "cheapest":
                    return _loadCatalog.LoadCheapest();
                case "newest":
                    return _loadCatalog.LoadNewest();
                case "sale":
                    return _loadCatalog.LoadBiggestSale();
                default:
                    return _loadCatalog.LoadAll().ToList();
            }
        }

        public async Task<Sale> LoadByIdAsync(int id)
        {
            var test = await _loadCatalog.LoadByIdAsync(id);
            return test;
        }

        public string InsertComment(int Id, string AuthorName, string Text, string IdUser = null)
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
            InsertUserFromComments(AuthorName);
            return _loadCatalog.InsertComment(comments);
        }

        public User CheckUserCookie(string cookie)
        {
            return _userRepository.GetUserByCookie(cookie);
        }

        public void InsertUserFromComments(string username)
        {
            string cookie = PasswordGenerator.Generate(length: 15, allowed: Sets.Alphanumerics);
            _userRepository.InsertUser(username, cookie);
        }
    }
}
