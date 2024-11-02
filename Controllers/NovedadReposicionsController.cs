using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlStock.Context;
using ControlStock.Models;

namespace ControlStock.Controllers
{
    public class NovedadReposicionsController : Controller
    {
        private readonly ControlStockContext _context;

        public NovedadReposicionsController(ControlStockContext context)
        {
            _context = context;
        }

        // GET: NovedadReposicions
        public async Task<IActionResult> Index()
        {
            var controlStockContext = _context.NovedadesReposiciones.Include(n => n.Producto);
            return View(await controlStockContext.ToListAsync());
        }

        // GET: NovedadReposicions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NovedadesReposiciones == null)
            {
                return NotFound();
            }

            var novedadReposicion = await _context.NovedadesReposiciones
                .Include(n => n.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (novedadReposicion == null)
            {
                return NotFound();
            }

            return View(novedadReposicion);
        }

        // GET: NovedadReposicions/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            return View();
        }

        // POST: NovedadReposicions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public async Task<IActionResult> Create([Bind("ProductoId,CantidadRepuesta,FechaReposicion,Descripcion")] NovedadReposicion novedadReposicion)
        {

            if (ModelState.IsValid)
            {
                var producto = _context.Productos.Find(novedadReposicion.ProductoId);
                if (producto != null)
                {
                    // Suma la cantidad de productos solicitada del stock
                    producto.Stock += novedadReposicion.CantidadRepuesta;

                    // Actualizar el producto en la base de datos
                    _context.Productos.Update(producto);
                }

                _context.Add(novedadReposicion);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", novedadReposicion.ProductoId);
            return View(novedadReposicion);
        }

        // GET: NovedadReposicions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NovedadesReposiciones == null)
            {
                return NotFound();
            }

            var novedadReposicion = await _context.NovedadesReposiciones.FindAsync(id);
            if (novedadReposicion == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", novedadReposicion.ProductoId);
            return View(novedadReposicion);
        }

        // POST: NovedadReposicions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductoId,CantidadRepuesta,FechaReposicion,Descripcion")] NovedadReposicion novedadReposicion)
        {
            if (id != novedadReposicion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novedadReposicion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NovedadReposicionExists(novedadReposicion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", novedadReposicion.ProductoId);
            return View(novedadReposicion);
        }

        // GET: NovedadReposicions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NovedadesReposiciones == null)
            {
                return NotFound();
            }

            var novedadReposicion = await _context.NovedadesReposiciones
                .Include(n => n.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (novedadReposicion == null)
            {
                return NotFound();
            }

            return View(novedadReposicion);
        }

        // POST: NovedadReposicions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NovedadesReposiciones == null)
            {
                return Problem("Entity set 'ControlStockContext.NovedadesReposiciones'  is null.");
            }
            var novedadReposicion = await _context.NovedadesReposiciones.FindAsync(id);
            if (novedadReposicion != null)
            {
                _context.NovedadesReposiciones.Remove(novedadReposicion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NovedadReposicionExists(int id)
        {
          return (_context.NovedadesReposiciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
