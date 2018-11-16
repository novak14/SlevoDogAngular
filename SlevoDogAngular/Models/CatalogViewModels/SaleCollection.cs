﻿using SlevoDogAngular.Models.CatalogViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlevoDogAngular.Models.CatalogViewModels
{
    public class SaleCollection
    {
        /// <summary>
        /// Collection of SaleViewModel
        /// </summary>
        public List<SaleViewModel> collections;

        public SaleCollection()
        {
            collections = new List<SaleViewModel>();
        }
    }
}
