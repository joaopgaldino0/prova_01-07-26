using System;
using TerceiraApi.Models;
using Microsoft.EntityFrameworkCore;

namespace joaopedro.api.Data;

public class AppDataContext : DbContext
{
    public DbSet<Pressao> PressaoArterial { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Banco.db");
    }

}