using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendFF.Models;
using Microsoft.EntityFrameworkCore;

namespace backendFF.Services.Context
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<OrganizationModel> OrganizationInfo { get; set; }
        public DbSet<YardModel> YardInfo { get; set; }
        public DbSet<TrailerModel> TrailerInfo { get; set; }
        public DbSet<UpdateLogModel> UpdateLog { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}