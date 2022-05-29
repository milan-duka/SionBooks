using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SionBooks.Attributes;
using SionBooks.Models;

namespace SionBooks.Areas.Admin.Models.ViewModels
{
    public class BookModels
    {
        public class CreateModel
        {
            public int Id { get; set; }

            [StringLength(500)]
            [Required]
            public string Title { get; set; }

            [Display(Name = "ISBN")]
            [StringLength(50)]
            [Required]
            public string Isbn { get; set; }

            [StringLength(2000)]
            [Required]
            public string Description { get; set; }

            [Required(ErrorMessage = "Please choose book image")]
            [Display(Name = "Book Cover")]
            [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
            public IFormFile Image { get; set; }

            [Display(Name = "Category")]
            [Required]
            public int CategoryId { get; set; }
            public virtual BookCategory Category { get; set; }
        }

        public class EditModel
        {
            public int Id { get; set; }

            [StringLength(500)]
            [Required]
            public string Title { get; set; }

            [Display(Name = "ISBN")]
            [StringLength(50)]
            [Required]
            public string Isbn { get; set; }

            [StringLength(2000)]
            [Required]
            public string Description { get; set; }

            [Display(Name = "Current Book Cover")]
            public string ImageUrl { get; set; }

            [Display(Name = "Change Book Cover")]
            [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
            public IFormFile Image { get; set; }

            [Display(Name = "Category")]
            [Required]
            public int CategoryId { get; set; }
            public virtual BookCategory Category { get; set; }
        }


    }
}
