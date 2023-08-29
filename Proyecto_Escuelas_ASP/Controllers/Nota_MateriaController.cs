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
    public class Nota_MateriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Nota_MateriaController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        // GET: Nota_Materia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NotaMaterias.Include("Estudiantes").Include("Materias").Where(n => n.Estado == true);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        // GET: Nota_Materia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota_Materia = await _context.NotaMaterias
                .Include(n => n.Estudiantes)
                .Include(n => n.Materias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota_Materia == null)
            {
                return NotFound();
            }

            return View(nota_Materia);
        }
        [HttpGet]
        // GET: Nota_Materia/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Estudiante = await _context.Estudiantes.Where(n => n.Estado == true).ToListAsync();
            ViewBag.Materia = await _context.Materias.Where(n => n.Estado == true).ToListAsync();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Nota_Materia nota_Materia)
        {
            nota_Materia.Estado = true;
            _context.Add(nota_Materia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        // GET: Nota_Materia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var nota_Materia = await _context.NotaMaterias
                  .Include(n => n.Estudiantes)
                  .Include(n => n.Materias)
                  .FirstOrDefaultAsync(m => m.Id == id);
            if (nota_Materia == null)
            {
                return NotFound();
            }
            ViewBag.Estudiante = await _context.Estudiantes.Where(n => n.Estado == true).ToListAsync();
            ViewBag.Materia = await _context.Materias.Where(n => n.Estado == true).ToListAsync();
            return View(nota_Materia);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Nota_Materia nota_Materia)
        {
            if(id == nota_Materia.Id)
            {
                nota_Materia.Estado = true;
                _context.NotaMaterias.Update(nota_Materia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nota_Materia);
        }
        [HttpGet]
        // GET: Nota_Materia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota_Materia = await _context.NotaMaterias
                .Include(n => n.Estudiantes)
                .Include(n => n.Materias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota_Materia == null)
            {
                return NotFound();
            }

            return View(nota_Materia);
        }

        // POST: Nota_Materia/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
           var notamateria = await _context.NotaMaterias.FindAsync(id);
            if (notamateria == null)
            {
                return NotFound();
            }
            notamateria.Estado = false;
            _context.NotaMaterias.Update(notamateria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Nota_MateriaExists(int id)
        {
          return (_context.NotaMaterias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
