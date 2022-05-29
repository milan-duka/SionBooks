using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SionBooks.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
