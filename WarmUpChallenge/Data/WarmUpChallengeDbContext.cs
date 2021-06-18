using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarmUpChallenge.Models;

namespace WarmUpChallenge.Data
{
    public class WarmUpChallengeDbContext : DbContext
    {
        public WarmUpChallengeDbContext(DbContextOptions<WarmUpChallengeDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
