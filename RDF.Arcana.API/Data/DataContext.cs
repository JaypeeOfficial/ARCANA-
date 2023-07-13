using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
}