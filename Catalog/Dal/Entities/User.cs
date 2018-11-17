using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class User
    {
        /// <summary>
        /// Unique identification of user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Username of user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Id in AspNetUser, used for identification
        /// </summary>
        public string UniqueString { get; set; }
    }
}
