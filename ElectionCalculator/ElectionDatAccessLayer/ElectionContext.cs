using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionCalculatorDataAccess
{
    public class ElectionContext : DbContext
    {
        public ElectionContext() : base("ElectionDBString")
        {
        }

        public DbSet<Vote> Votes { get; set; }
    }
}