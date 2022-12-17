using Micro_social_platform.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Micro_social_platform.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet <Article> Articles { get; set; }
        public DbSet <Comment> Comments { get; set; }
        public DbSet <Group> Groups { get; set; }
        public DbSet <GroupMessage> GroupMessages { get; set; }
        public DbSet <Profile> Profiles { get; set; }
    }
}