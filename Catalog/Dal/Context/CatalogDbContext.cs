using Catalog.Configuration;
using Catalog.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Context
{
    public class CatalogDbContext : DbContext
    {
        private readonly CatalogOptions _catalogOptions;

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IOptions<CatalogOptions> catalogOptions) : base(options)
        {
            if (catalogOptions == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _catalogOptions = catalogOptions.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_catalogOptions.connectionString);
        }

        public DbSet<Sale> Sale { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Comments>()
            //    .HasOne(p => p.Sale)
            //    .WithMany(b => b.Comments)
            //    .HasForeignKey(c => c.FkSale);

            modelBuilder.Entity<Sale>()
                .HasMany(a => a.Comments)
                .WithOne(b => b.Sale)
                .HasForeignKey(c => c.FkSale);

            base.OnModelCreating(modelBuilder);
        }
    }
}
