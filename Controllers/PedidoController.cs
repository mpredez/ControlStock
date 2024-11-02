using ControlStock.Context;
using ControlStock.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class PedidoController : Controller
{
    private readonly ControlStockContext _context;
    

    public PedidoController(ControlStockContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        // Cargar productos disponibles
        var productos = _context.Productos.ToList();
        ViewBag.Productos = productos; // Pasar productos a la vista
        return View(new Pedido()); // Regresar una nueva instancia de Pedido
    }

    [HttpPost]
    public IActionResult Create(Pedido pedido, string detallePedidoJson)
    {
        if (ModelState.IsValid)
        {
            // Deserializar el JSON a una lista de DetallePedido
            var detallePedidos = JsonConvert.DeserializeObject<List<DetallePedido>>(detallePedidoJson);

            // Agregar los detalles al pedido
            pedido.Detalles = detallePedidos;

            // Actualizar el stock de cada producto en el detalle del pedido
            foreach (var detalle in detallePedidos)
            {
                var producto = _context.Productos.Find(detalle.ProductoId);
                if (producto != null)
                {
                    // Resta la cantidad de productos solicitada del stock
                    producto.Stock -= detalle.Cantidad;

                    // Asegurarse de que el stock no quede en negativo
                    if (producto.Stock < 0)
                    {
                        ModelState.AddModelError("", $"El stock del producto {producto.Nombre} no es suficiente.");
                        return View(pedido);
                    }

                    // Actualizar el producto en la base de datos
                    _context.Productos.Update(producto);
                }
            }

            // Guardar el pedido en la base de datos
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
          //  return RedirectToAction("Index");
        }

        // Si el ModelState no es válido, devolver a la vista con los errores
        var productos = _context.Productos.ToList();
        ViewBag.Productos = productos; // Pasar productos a la vista
        return View(new Pedido()); // Regresar una nueva instancia de Pedido

    }
}
