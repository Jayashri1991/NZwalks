using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDBContext : IdentityDbContext
    {
        public NZWalksAuthDBContext(DbContextOptions<NZWalksAuthDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerroleid = "3f474e07-f753-41cf-9978-39e3a137fb06";
            var writerroleid = "1ae5eb09-93cd-4d50-b863-4dbfdab50e47";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerroleid,
                    ConcurrencyStamp=readerroleid,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id=writerroleid,
                    ConcurrencyStamp =writerroleid,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
