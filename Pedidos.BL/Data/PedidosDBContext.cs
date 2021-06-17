using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pedidos.BL.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Pedidos.BL.Data
{
    public partial class PedidosDBContext : DbContext
    {
        private static PedidosDBContext pedidosDBContext = null;
        public PedidosDBContext()
        {
        }
        public PedidosDBContext(
            DbContextOptions<PedidosDBContext> options) : base(options) { }

        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<PedidoProducto> PedidoProductos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        //public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    {
            if (!optionsBuilder.IsConfigured)
            {
                /* #warning To protect potentially sensitive information in your connection string, you should move it out of source code. 
                 * You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. 
                 * For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263. */
                optionsBuilder.UseSqlServer("Server=(local)\\SQLExpress;Database=PedidosDB;Trusted_Connection=True;");
                // "PedidoContext"

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedido");

                entity.Property(e => e.Cliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PedidoProducto>(entity =>
            {
                entity.HasKey(e => new { e.PedidoId, e.ProductoId });

                entity.ToTable("Pedido_Producto");

                entity.Property(e => e.PedidoId).HasColumnName("pedido_Id");

                entity.Property(e => e.ProductoId).HasColumnName("producto_Id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Fecha).HasColumnType("datetime");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public static PedidosDBContext Create()
        {
            if (pedidosDBContext == null)
                pedidosDBContext = new PedidosDBContext();
            return pedidosDBContext;
        }
    }
}
