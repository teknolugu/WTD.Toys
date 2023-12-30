using kDg.FileBaseContext.Extensions;
using Microsoft.EntityFrameworkCore;
using WinToys.DataSource.Entities.EF;
using WinToys.Models;

namespace WinToys.DataSource;

public class EfDbContext : DbContext
{
    public DbSet<BrowserPathEntity> BrowserPath { get; set; }
    public DbSet<BrowserMapEntity> BrowserMap { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseFileBaseContextDatabase(EnvVar.PathFileDb)
            .EnableSensitiveDataLogging();
    }
}