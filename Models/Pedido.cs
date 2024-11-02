using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlStock.Models
{
    public class Pedido
    {

        public int Id { get; set; }
        public string Cliente { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

    }
}
