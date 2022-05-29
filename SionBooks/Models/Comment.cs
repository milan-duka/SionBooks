using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SionBooks.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Display(Name = "Comment")]
        public string text { get; set; }
        public virtual Book book { get; set; }
        public virtual User user { get; set; }
    }
}
