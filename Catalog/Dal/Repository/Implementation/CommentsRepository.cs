using Catalog.Configuration;
using Catalog.Dal.Repository.Abstraction;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
