using AcunMedyaAkademiWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcunMedyaAkademiWebAPI.Context
{
    public class WebAPIDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-8TRT9BV\\MSSQLSERVER01; initial Catalog=DbAcunMedyaWebApi; integrated Security=true; TrustServerCertificate=true");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }

}
