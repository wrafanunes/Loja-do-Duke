using Loja_do_Duke.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja_do_Duke.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserSupply> ApplicationUserSupplies { get; set; }
    }
}
