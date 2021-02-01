using Microsoft.EntityFrameworkCore;
using EFProductControl.Domain;
using EFProductControl.Data.Configuration;

namespace EFProductControl.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=productControl.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());

            modelBuilder.Entity<Produto>(p =>
            {
                p.ToTable("Produtos");
                p.HasKey(p => p.Id);
                p.Property(p => p.CodigoDeBarras).HasColumnType("VARCHAR(60)");
                p.Property(p => p.Descricao).HasColumnType("VARCHAR(60)");
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.TipoProduto).HasConversion<string>();
            });

            modelBuilder.Entity<Pedido>(p =>
            {
                p.ToTable("Pedidos");
                p.HasKey(p => p.Id);
                p.Property(p => p.IniciadoEm).HasDefaultValue("NOW()").ValueGeneratedOnAdd();
                p.Property(p => p.Status).HasConversion<string>();
                p.Property(p => p.TipoFrete).HasConversion<string>();
                p.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

                p.HasMany(p => p.Itens)
                    .WithOne(p => p.Pedido)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PedidoItem>(p =>
            {
                p.ToTable("PedidoItens");
                p.HasKey(p => p.Id);
                p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
                p.Property(p => p.Valor);
            });
        }
    }
}
