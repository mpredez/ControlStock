namespace ControlStock.Models
{
    public class PedidoViewModel
    {
        public Pedido Pedido { get; set; }
        public List<Producto> Producto { get; set; }
        public List<DetallePedido> DetallePedido { get; set; } = new List<DetallePedido>();
    }
}
