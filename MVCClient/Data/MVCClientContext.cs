using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCClient.Models;

namespace MVCClient.Data
{
    public class MVCClientContext : DbContext
    {
        public MVCClientContext (DbContextOptions<MVCClientContext> options)
            : base(options)
        {
        }

        public DbSet<MVCClient.Models.Customers> Customer { get; set; }

        public DbSet<MVCClient.Models.CustomerType> CustomerType { get; set; }

        public DbSet<MVCClient.Models.Activities> Activities { get; set; }

        public DbSet<MVCClient.Models.ActivityType> ActivityType { get; set; }
    }
}
