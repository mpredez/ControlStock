using ControlStock.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlStock.Context
{
    public class ControlStockContext : DbContext
    {
        public ControlStockContext(DbContextOptions<ControlStockContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedidos { get; set; }
        public DbSet<NovedadReposicion> NovedadesReposiciones { get; set; }
        public DbSet<NotificacionStock> NotificacionesStock { get; set; }
    }
}
