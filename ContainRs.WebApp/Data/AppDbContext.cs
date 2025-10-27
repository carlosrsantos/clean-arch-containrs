using System.Linq.Expressions;
using ContainRs.Application.Repositories;
using ContainRs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ContainRs.WebApp.Data;

public class AppDbContext : DbContext, IClienteRepository
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>()
        .HasKey(c => c.Id);

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Nome).IsRequired();
        
        modelBuilder.Entity<Cliente>()
            .OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Value)
                     .HasColumnName("Email")
                     .IsRequired();
            });

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Estado)
            .HasConversion<string>();

        modelBuilder.Entity<Cliente>()
            .Property(c => c.CPF).IsRequired();
    }

    public async Task<Cliente> AddAsync(Cliente cliente)
    {
        await Clientes.AddAsync(cliente);
        await SaveChangesAsync();
        return cliente;
    }

    public async Task<IEnumerable<Cliente>> GetAsync(Expression<Func<Cliente, bool>>? filtro = null)
    {
        IQueryable<Cliente> queryClientes = Clientes;
        if (filtro != null)
        {
            queryClientes = queryClientes.Where(filtro);
        }

        return await queryClientes
            .AsNoTracking()
            .ToListAsync();
    }
}
