using Microsoft.AspNetCore.Http;
using SionBooks.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SionBooks.Models.ViewModels
{
    public class SearchModels
    {
        public class SearchModel
        {

            public string Title { get; set; }

            [Display(Name = "Category")]
            public int CategoryId { get; set; }
            public virtual BookCategory Category { get; set; }

            [Display(Name = "ISBN")]
            public string Isbn { get; set; }
        }

        public class SearchResultModel
        {
            public int Id { get; set; }
            public string Title { get; set; }

            [Display(Name = "Category")]
            public int CategoryId { get; set; }
            public virtual BookCategory Category { get; set; }

            [Display(Name = "ISBN")]
            public string Isbn { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            
            [Display(Name = "Votes")]
            public int Count { get; set; }
            public string Voted { get; set; }
        }
    }
}
