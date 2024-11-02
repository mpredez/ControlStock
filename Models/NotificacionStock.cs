namespace ControlStock.Models
{
    public class NotificacionStock
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int StockActual { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
    }
}
