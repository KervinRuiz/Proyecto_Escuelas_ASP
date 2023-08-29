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
    public class ProfesorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfesorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        // GET: Profesors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Profesores.Include("Personas").Where(n => n.Estado == true);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpGet]
        // GET: Profesors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var profesor = await _context.Profesores
                .Include(p => p.Personas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }
        [HttpGet]
        // GET: Profesors/Create
        public IActionResult Create()
        {
            var sexo = Enum.GetNames(typeof(Enums.Enums.Sexo));
            ViewData["Sexo"] = new SelectList(sexo);
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Profesor profesor)
        {
                profesor.Personas.Estado = true;
                profesor.Personas.Fecha_Creacion = DateTime.Now;
                profesor.Estado = true;
                profesor.FechaCreacion = DateTime.Now;
                _context.Add(profesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            return View(profesor);
        }
        [HttpGet]
        // GET: Profesors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var profesor = await _context.Profesores
                .Include(p => p.Personas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }
            var sexo = Enum.GetNames(typeof(Enums.Enums.Sexo));
            ViewData["Sexo"] = new SelectList(sexo);
            return View(profesor);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Profesor profesor)
        {
           if(id == profesor.Id)
            {
                profesor.Estado = true;
                _context.Profesores.Update(profesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return View(profesor);
        }
        [HttpGet]
        // GET: Profesors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var profesor = await _context.Profesores
                .Include(p => p.Personas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if(profesor == null)
            {
                return NotFound();
            }
            profesor.Estado = false;
            _context.Profesores.Update(profesor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
          return (_context.Profesores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
