using Microsoft.EntityFrameworkCore;
using TestApplication.DAL.Entities;

namespace TestApplication.DAL.Core;

public class MyTestApplicationContext : DbContext
{
    public MyTestApplicationContext(DbContextOptions<MyTestApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<TableA>? TableA { get; }

    public DbSet<TableB>? TableB { get; }

    public DbSet<TableC>? TableC { get; }
}
