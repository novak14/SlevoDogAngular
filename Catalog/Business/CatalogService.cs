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

        public async Task<List<Sale>> GetCategoryItems(int categoryId)
        {
            return await _saleRepository.GetCategoryItems(categoryId);
        }

        public async Task<Sale> LoadByIdAsync(int id)
        {
            var test = await _saleRepository.LoadByIdAsync(id);
            return test;
        }

        public async Task AddRankForSale(int saleId, int rank)
        {
            await _saleRepository.AddRank(saleId, rank);
        }

        public async Task<string> InsertCommentAsync(string AuthorName, string Text, int Id, int? ParentCommentId, string IdUser = null)
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

            return await InsertUserFromCommentsAsync(AuthorName);
        }

        public async Task AddRankForComment(int commentId, int rank)
        {
            await _commentRepository.AddRank(commentId, rank);
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
            var comments = await _commentRepository.GetCommentsAsync(saleId);

            

            return comments;
        }

        public string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("about {0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("about {0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return String.Format("about {0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return String.Format("about {0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("about {0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("about {0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }
    }
}
