using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SionBooks.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(250)]
        [Required]
        public string userName { get; set; }

        [StringLength(100)]
        [EmailAddress]
        [Required]
        public string email { get; set; }

        [StringLength(100, MinimumLength = 6)]
        [Required]
        public string password { get; set; }
        public bool isAdmin { get; set; }
        public bool isDeleted { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
