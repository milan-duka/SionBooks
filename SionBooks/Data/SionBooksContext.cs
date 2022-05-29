using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SionBooks.Models;

namespace SionBooks.Data
{
    public class SionBooksContext : DbContext
    {
        public SionBooksContext(DbContextOptions<SionBooksContext> options)
            : base(options)
        {
        }

        public DbSet<SionBooks.Models.User> User { get; set; }

        public DbSet<SionBooks.Models.Book> Book { get; set; }

        public DbSet<SionBooks.Models.BookCategory> Category { get; set; }

        public DbSet<SionBooks.Models.Vote> Vote { get; set; }
        public DbSet<SionBooks.Models.Comment> Comment { get; set; }
    }
}
