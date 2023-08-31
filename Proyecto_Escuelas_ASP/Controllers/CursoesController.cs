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
    public class CursoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursoesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        // GET: Cursoes
        public async Task<IActionResult> Index()
        {
              var curso = _context.Cursos.Where(n => n.Estado == true);
              return View(await curso.ToListAsync());
        }
        [HttpGet]
        // GET: Cursoes
        public async Task<IActionResult> Index2()
        {
            var curso = _context.Cursos.Where(n => n.Estado == true);
            return View(await curso.ToListAsync());
        }

        [HttpGet]
        // GET: Cursoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var curso = _context.Cursos.Find(id);
            if(curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        [HttpGet]
        // GET: Cursoes/Create
        public IActionResult Create()
        {
            var tiponivel = Enum.GetNames(typeof(Enums.Enums.TipoNivel));
            ViewData["Nivel"] = new SelectList(tiponivel);
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Curso curso)
        {
            if (ModelState.IsValid)
            {
                curso.FechaRegistro = DateTime.Now;
                curso.Estado = true;
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }
        [HttpGet]
        // GET: Cursoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           if(id ==null)
            {
                return NotFound();
            }
            var curso = _context.Cursos.Find(id);
            if(curso == null)
            {
                return NotFound();
            }
            var tiponivel = Enum.GetNames(typeof(Enums.Enums.TipoNivel));
            ViewData["Nivel"] = new SelectList(tiponivel);
            return View(curso);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Curso curso)
        {
            
                if(id == curso.Id)
                {
                    curso.Estado = true;
                    _context.Cursos.Update(curso);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            
            return View(curso);
        }
        [HttpGet]
        // GET: Cursoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var curso = _context.Cursos.Find(id);
            if(curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if( curso == null)
            {
                return NotFound();
            }
            curso.Estado = false;
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
          return (_context.Cursos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
