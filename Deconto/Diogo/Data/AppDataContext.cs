using Diogo.Models;
using Microsoft.EntityFrameworkCore;

namespace Diogo.Data;

public class AppDataContext : DbContext
{
    public DbSet<FolhaPagamento> Folhas { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Deconto_Diogo.db");
    }
}
