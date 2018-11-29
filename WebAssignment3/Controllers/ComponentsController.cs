using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAssignment3.Data;
using WebAssignment3.Models;
using WebAssignment3.ViewModels;

namespace WebAssignment3.Controllers
{
    [Authorize("AdminPolicy")]
    public class ComponentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ComponentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Component
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IList<ComponentViewModel> vm = _mapper.Map<IList<ComponentViewModel>>(await _context.Component.ToListAsync());
            return View(vm);
        }

        // GET: Component/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Component
                .FirstOrDefaultAsync(m => m.ComponentId == id);

            if (component == null)
            {
                return NotFound();
            }

            ComponentViewModel vm = _mapper.Map<ComponentViewModel>(component);

            return View(vm);
        }

        // GET: Component/Create
        public IActionResult Create()
        {
            ComponentViewModel vm = new ComponentViewModel
            {
                ComponentTypeIdsSelect = new SelectList(_context.ComponentType.ToList(), "ComponentTypeId", "ComponentName"),
            };

            return View(vm);
        }

        // POST: Component/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,ComponentTypeId,ComponentNumber,SerialNo,Status,AdminComment,UserComment,CurrentLoanInformationId")] ComponentViewModel vm)
        {
            Component temp = _mapper.Map<Component>(vm);

            if (ModelState.IsValid)
            {
                _context.Add(temp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Component/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Component.FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            ComponentViewModel vm = _mapper.Map<ComponentViewModel>(component);
            vm.ComponentTypeIdsSelect = new SelectList(_context.ComponentType.ToList(), "ComponentTypeId", "ComponentName");

            return View(vm);
        }

        // POST: Component/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ComponentId,ComponentTypeId,ComponentNumber,SerialNo,Status,AdminComment,UserComment,CurrentLoanInformationId")] ComponentViewModel vm)
        {
            if (id != vm.ComponentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Component component = _mapper.Map<Component>(vm);

                try
                {
                    _context.Update(component);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(component.ComponentId))
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
            return View(vm);
        }

        // GET: Component/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Component
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Component/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var component = await _context.Component.FindAsync(id);
            _context.Component.Remove(component);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentExists(long id)
        {
            return _context.Component.Any(e => e.ComponentId == id);
        }
    }
}
