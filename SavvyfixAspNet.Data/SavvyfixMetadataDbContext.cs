using Microsoft.EntityFrameworkCore;
using SavvyfixAspNet.Domain.Entities;


namespace SavvyfixAspNet.Data;

public class SavvyfixMetadataDbContext : DbContext
{  
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Atividades> Atividades { get; set; }
    public DbSet<Compra> Compras { get; set; }
 
    public SavvyfixMetadataDbContext(DbContextOptions<SavvyfixMetadataDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compra>()
            .HasOne(c => c.Produto)           
            .WithMany(p => p.Compras)          
            .HasForeignKey(c => c.IdProd); 
        
        modelBuilder.Entity<Compra>()
            .HasOne(c => c.Atividades)           
            .WithMany(p => p.Compras)          
            .HasForeignKey(c => c.IdAtividades);
        
        modelBuilder.Entity<Compra>()
            .HasOne(c => c.Cliente)           
            .WithMany(p => p.Compras)          
            .HasForeignKey(c => c.IdCliente); 
        
        modelBuilder.Entity<Atividades>()
            .HasOne(c => c.Cliente)           
            .WithMany(p => p.Atividades)          
            .HasForeignKey(c => c.IdCliente); 
        
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Endereco)           
            .WithMany(p => p.Clientes)          
            .HasForeignKey(c => c.IdEndereco); 
        
        
    }
 
    
}