using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RDF.Arcana.API.Domain;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RDF.Arcana.API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Items> Items { get; set; }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<MeatType> MeatTypes { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<ProductSubCategory> ProductSubCategories { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Uom> Uoms { get; set; }
    public virtual DbSet<MainMenu> MainMenus { get; set; }
    public virtual DbSet<Module> Modules { get; set; }
    public virtual DbSet<UserRoles> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
       modelBuilder.Entity<UserRoles>()
           .Property(e => e.Permissions)
           .HasConversion(
               v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
               v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null),
               new ValueComparer<ICollection<string>>(
                   (c1, c2) => c1.SequenceEqual(c2),
                   c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                   c => c.ToList()));
       
       modelBuilder.Entity<UserRoles>()
           .HasOne(ur => ur.User)
           .WithOne(u => u.UserRoles)
           .HasForeignKey<User>(u => u.UserRoleId);
    }
}