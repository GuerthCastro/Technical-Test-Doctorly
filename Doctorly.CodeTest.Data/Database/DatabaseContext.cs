using System;
using System.Collections.Generic;
using System.Text;
using Doctorly.CodeTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Doctorly.CodeTest.Data
{
    public class DatabaseContext : DbContext
    {
        //Note
        /*
            for this test app, the EF escafilding for  sqlite don work properly in .net core and the best way to implememt it manually
        */
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries{ get; set; }
        IConfiguration Configuration { get; }
        public DatabaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conectionsString = Configuration.GetConnectionString("DefaultConnectionString");
            optionsBuilder.UseSqlite($"Filename={conectionsString}");
        }
    }
}
