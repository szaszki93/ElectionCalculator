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
        public DbSet<Vote> Votes { get; set; }
    }
}