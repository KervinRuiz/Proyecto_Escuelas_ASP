using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Escuelas_ASP.Data;
using Proyecto_Escuelas_ASP.Models;

namespace Proyecto_Escuelas_ASP.Controllers
{
    public class MateriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MateriasController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        // GET: Materias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Materias.Include("Profesores").Where(n => n.Estado == true);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpGet]
        // GET: Materias/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias
                .Include(m => m.Profesores)
                .Include(m => m.Profesores.Personas)
                .FirstOrDefaultAsync(m => m.Nombre_Materia == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }
        [HttpGet]
        // GET: Materias/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Profesor = await _context.Profesores.Where(n => n.Estado == true).ToListAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Materia materia)
        {
            materia.Estado = true;
            materia.FechaCreacion = DateTime.Now;
            _context.Add(materia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        // GET: Materias/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var materia = await _context.Materias
               .Include(m => m.Profesores)
               .Include(m => m.Profesores.Personas)
               .FirstOrDefaultAsync(m => m.Nombre_Materia == id);
            if (materia == null)
            {
                return NotFound();
            }
            ViewBag.Profesor = await _context.Profesores.Where(n => n.Estado == true).ToListAsync();
            return View(materia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Materia materia)
        {
            if (id == null)
            {
                materia.Estado = true;
                _context.Materias.Update(materia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materia);
        }
        [HttpGet]
        // GET: Materias/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias
                .Include(m => m.Profesores)
                .FirstOrDefaultAsync(m => m.Nombre_Materia == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        // POST: Materias/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }
            materia.Estado = false;
            _context.Materias.Update(materia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaExists(string id)
        {
            return (_context.Materias?.Any(e => e.Nombre_Materia == id)).GetValueOrDefault();
        }
    }
}
