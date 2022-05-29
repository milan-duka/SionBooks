using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SionBooks.Models
{
    public class Book
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

        [Display(Name = "Book cover")]
        [Required]
        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }
        public virtual BookCategory Category { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
