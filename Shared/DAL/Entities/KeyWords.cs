using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dal.Entities
{
    public class KeyWords
    {
        /// <summary>
        /// Unique identification of KeyWord
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Adjusted keyword for searching
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Full name of keyword with spaces and everything
        /// </summary>
        public string FullKeyword { get; set; }
    }
}
