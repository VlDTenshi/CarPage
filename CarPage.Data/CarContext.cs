using CarPage.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarPage.Data
{
    public class CarContext : DbContext
    {
        public CarContext
            (
            DbContextOptions<CarContext> options
            ) : base(options) { }
        public DbSet<CarItem> CarItems { get; set; }
        public DbSet<FileToDatabase1> FileToDatabases1 { get; set; }
    }
}
