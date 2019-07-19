using DataGenerator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<LastName> LastNames { get; set; }
        public DbSet<FirstName> FirstNames { get; set; }
        public DbSet<Domains> Domains { get; set; }
    }
}
