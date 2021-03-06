﻿using Catalog.Configuration;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Repository.Implementation
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly CatalogOptions _options;

        public CommentsRepository(IOptions<CatalogOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value;
        }

        public async Task<List<Comments>> GetCommentsAsync(int saleId)
        {
            using (var connection = new SqlConnection(_options.connectionString))
            {
                return (await connection.QueryAsync<Comments>("SELECT * FROM Comments WHERE FkSale = @Id order by Rank desc, DateInsert desc", new { Id = saleId })).ToList();
            }
        }

        public async Task InsertCommentAsync(Comments comments)
        {
            string sql = @"INSERT INTO Comments(FkSale, DateInsert, FkUser, Name, Rank, Text, FkParrentComment, Disabled) 
                            VALUES(@FkSale, @DateInsert, @FkUser, @Name, @Rank, @Text, @FkParrentComment, @Disabled);";

            try
            {
                using (var connection = new SqlConnection(_options.connectionString))
                {
                    var affRows = await connection.ExecuteAsync(sql, new
                    {
                        FkSale = comments.FkSale,
                        DateInsert = comments.DateInsert,
                        FkUser = comments.FkUser,
                        Name = comments.Name,
                        Rank = comments.Rank,
                        Text = comments.Text,
                        FkParrentComment = comments.FkParrentComment,
                        Disabled = comments.Disabled
                    });
                }
            }
            catch (Exception e)
            {
                var ts = e;
            }
        }

        public async Task AddRank(int commentId, int rank)
        {
            string sql = @"UPDATE Comments SET Rank = @Rank WHERE Id = @Id;";

            try
            {
                using (var connection = new SqlConnection(_options.connectionString))
                {
                    var affRows = await connection.ExecuteAsync(sql, new { Rank = rank, Id = commentId });
                }
            }
            catch (Exception e)
            {
                var ts = e;
            }
        }

        public async Task<int> CheckRankUser(int commentId, int userId)
        {
            string sql = @"SELECT COUNT(*) AS Amount FROM RankCommentsUser WHERE FkComment = @CommentId AND FkUser = @UserId";
            int check;
            using (var connection = new SqlConnection(_options.connectionString))
            {
                await connection.OpenAsync();
                var rows = await connection.QueryFirstOrDefaultAsync(sql, new { CommentId = commentId, UserId = userId });
                check = (int)rows.Amount;
            }
            return check;
        }

        public async Task ConnectUserRank(int commentId, int userId)
        {
            string sql = @"INSERT INTO RankCommentsUser(FkComment, FkUser) VALUES (@FkComment, @FkUser);";

            try
            {
                using (var connection = new SqlConnection(_options.connectionString))
                {
                    var affRows = await connection.ExecuteAsync(sql, new { FkComment = commentId, FkUser = userId });
                }
            }
            catch (Exception e)
            {
                var ts = e;
            }
        }
    }
}
