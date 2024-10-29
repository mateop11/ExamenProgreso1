using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenProgreso1.Data;
using ExamenProgreso1.Models;

namespace ExamenProgreso1.Controllers
{
    public class MPillajoesController : Controller
    {
        private readonly ExamenProgreso1Context _context;

        public MPillajoesController(ExamenProgreso1Context context)
        {
            _context = context;
        }

        // GET: MPillajoes
        public async Task<IActionResult> Index()
        {
            var examenProgreso1Context = _context.MPillajo.Include(m => m.Celular);
            return View(await examenProgreso1Context.ToListAsync());
        }

        // GET: MPillajoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mPillajo = await _context.MPillajo
                .Include(m => m.Celular)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mPillajo == null)
            {
                return NotFound();
            }

            return View(mPillajo);
        }

        // GET: MPillajoes/Create
        public IActionResult Create()
        {
            ViewData["CelularId"] = new SelectList(_context.Celular, "Id", "Modelo");
            return View();
        }

        // POST: MPillajoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Salario,Edad,Activo,FechaNacimiento,CelularId")] MPillajo mPillajo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mPillajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CelularId"] = new SelectList(_context.Celular, "Id", "Modelo", mPillajo.CelularId);
            return View(mPillajo);
        }

        // GET: MPillajoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mPillajo = await _context.MPillajo.FindAsync(id);
            if (mPillajo == null)
            {
                return NotFound();
            }
            ViewData["CelularId"] = new SelectList(_context.Celular, "Id", "Modelo", mPillajo.CelularId);
            return View(mPillajo);
        }

        // POST: MPillajoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Salario,Edad,Activo,FechaNacimiento,CelularId")] MPillajo mPillajo)
        {
            if (id != mPillajo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mPillajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MPillajoExists(mPillajo.Id))
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
            ViewData["CelularId"] = new SelectList(_context.Celular, "Id", "Modelo", mPillajo.CelularId);
            return View(mPillajo);
        }

        // GET: MPillajoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mPillajo = await _context.MPillajo
                .Include(m => m.Celular)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mPillajo == null)
            {
                return NotFound();
            }

            return View(mPillajo);
        }

        // POST: MPillajoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mPillajo = await _context.MPillajo.FindAsync(id);
            if (mPillajo != null)
            {
                _context.MPillajo.Remove(mPillajo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MPillajoExists(int id)
        {
            return _context.MPillajo.Any(e => e.Id == id);
        }
    }
}
