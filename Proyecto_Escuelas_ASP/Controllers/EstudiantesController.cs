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
    public class EstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var estudiante = _context.Estudiantes.Include("Personas").Include("Cursos").Where(n => n.Estado == true);
            return View(await estudiante.ToListAsync());
        }
        [HttpGet]
        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var estudiante = await _context.Estudiantes
               .Include(e => e.Cursos)
               .Include(e => e.Personas)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }
        [HttpGet]
        // GET: Estudiantes/Create
        public async Task<IActionResult> Create()
        {
            var sexo = Enum.GetNames(typeof(Enums.Enums.Sexo));
            ViewData["Sexo"] = new SelectList(sexo);
            ViewBag.Curso = await _context.Cursos.Where(n => n.Estado == true).ToListAsync();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudiante estudiante)
        {   
                estudiante.Personas.Estado = true;
                estudiante.Personas.Fecha_Creacion = DateTime.Now;
                estudiante.FechaRegistro = DateTime.Now;
                estudiante.Estado = true;
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(estudiante);
        }
        [HttpGet]
        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
           if(id == null)
            {
                return NotFound();
            }
            var estudiante = await _context.Estudiantes
               .Include(e => e.Cursos)
               .Include(e => e.Personas)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }
            var sexo = Enum.GetNames(typeof(Enums.Enums.Sexo));
            ViewData["Sexo"] = new SelectList(sexo);
            ViewBag.Curso = await _context.Cursos.Where(n => n.Estado == true).ToListAsync();
            return View(estudiante);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Estudiante estudiante)
        {
            if(id == estudiante.Id)
            {
                estudiante.Estado = true;
                _context.Estudiantes.Update(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }
        [HttpGet]
        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.Cursos)
                .Include(e => e.Personas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            estudiante.Estado = false;
            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
          return (_context.Estudiantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
