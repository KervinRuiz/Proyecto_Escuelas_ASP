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
    public class CursoMateriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursoMateriasController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        // GET: CursoMaterias
        public async Task<IActionResult> Index()
        {
            var cursoMaterias = _context.CursoMaterias.Include("Materias").Include("Cursos").Where(n => n.Estado == true);
            return View(await cursoMaterias.ToListAsync());
        }
        [HttpGet]
        // GET: CursoMaterias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoMateria = await _context.CursoMaterias
                .Include(c => c.Cursos)
                .Include(c => c.Materias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursoMateria == null)
            {
                return NotFound();
            }

            return View(cursoMateria);
        }
        [HttpGet]
        // GET: CursoMaterias/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Curso = await _context.Cursos.Where(n => n.Estado == true).ToListAsync();
            ViewBag.Materia = await _context.Materias.Where(n => n.Estado == true).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CursoMateria cursoMateria)
        {
                cursoMateria.Estado = true;
                _context.Add(cursoMateria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(cursoMateria);
        }
        [HttpGet]
        // GET: CursoMaterias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoMateria = await _context.CursoMaterias
                .Include(c => c.Cursos)
                .Include(c => c.Materias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursoMateria == null)
            {
                return NotFound();
            }
            ViewBag.Curso = await _context.Cursos.Where(n => n.Estado == true).ToListAsync();
            ViewBag.Materia = await _context.Materias.Where(n => n.Estado == true).ToListAsync();
            return View(cursoMateria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CursoMateria cursoMateria)
        {
            if(id == cursoMateria.Id)
            {
                cursoMateria.Estado = true;
                _context.CursoMaterias.Update(cursoMateria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cursoMateria);
        }
        [HttpGet]
        // GET: CursoMaterias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoMateria = await _context.CursoMaterias
                .Include(c => c.Cursos)
                .Include(c => c.Materias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursoMateria == null)
            {
                return NotFound();
            }

            return View(cursoMateria);
        }

        // POST: CursoMaterias/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var cursoMateria = await _context.CursoMaterias.FindAsync(id);
            if (cursoMateria == null)
            {
                return NotFound();
            }
            cursoMateria.Estado = false;
            _context.CursoMaterias.Update(cursoMateria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoMateriaExists(int id)
        {
          return (_context.CursoMaterias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
