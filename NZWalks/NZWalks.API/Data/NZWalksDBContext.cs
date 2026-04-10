using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> DbContextOptions) : base(DbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Images> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Seed the data for difiiculty
            //Easy, Medium ,hard

            var difficulties = new List<Difficulty>() {
                new Difficulty
                {
                    ID=Guid.Parse("574c0dcc-d832-4617-b096-db152effd90f"),
                    Name="Easy"
                },
                  new Difficulty
                {
                    ID=Guid.Parse("628dd14a-a0aa-4283-baf0-a9b4e8b0a16d"),
                    Name="Medium"
                },
                    new Difficulty
                {
                    ID=Guid.Parse("c19aed72-0048-4740-9f04-14f46338b1bf"),
                    Name="Hard"
                }

            };
            //send difficulties to database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
            {
                new Region
                {
                    ID=Guid.Parse("7ee26529-e3d8-40e8-9263-9fbbef958aef"),
                    Code="IN-AP",
                    Name="Andhra Pradesh",
                },
                 new Region
                {
                   ID=Guid.Parse("df363e96-9143-4939-b463-67d5a3f40984"),
                    Code="IN-BR",
                    Name="Bihār",
                },
                  new Region
                {
                      ID=Guid.Parse("642ee894-7bb4-4950-a429-a837ed9d5386"),
                    Code="IN-GA",
                    Name="Goa",
                },
                    new Region
                {
                     ID=Guid.Parse("8a58170e-d4ed-4c19-b24e-e5c341be474f"),
                    Code="IN-OD",
                    Name="Odisha",
                }
            };
            //send region to Database

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
