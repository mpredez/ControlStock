namespace ControlStock.Models
{
    public class NovedadReposicion
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
        public int CantidadRepuesta { get; set; }
        public DateTime FechaReposicion { get; set; }
        public string? Descripcion { get; set; }
    }
}
