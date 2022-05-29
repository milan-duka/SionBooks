using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SionBooks.Models
{
    public class BookCategory
    {
        public int Id { get; set; }

        [StringLength(200)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
