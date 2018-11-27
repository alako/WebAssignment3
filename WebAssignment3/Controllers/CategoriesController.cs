using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }

        // GET: Category/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel vm = new CategoryViewModel
            {
                Name = category.Name,
                CategoryId = category.CategoryId.ToString(),
                DisplayComponentTypes = _context.ComponentTypeCategory
                                                .Where(i => i.CategoryId == category.CategoryId)
                                                .Select(ctc => ctc.ComponentType).ToList(),
            };

            return View(vm);
        }

        // GET: Category/Create
        public async Task<IActionResult> Create()
        {
            CategoryViewModel vm = new CategoryViewModel();

            List<SelectListItem> componentTypesAsSelectList = await _context.ComponentType.Select(ct =>
                new SelectListItem()
                {
                    Value = ct.ComponentTypeId.ToString(),
                    Text = ct.ComponentName
                }
            ).ToListAsync();

            vm.ComponentTypes = new MultiSelectList(componentTypesAsSelectList.OrderBy(ct => ct.Text), "Value", "Text");

            return View(vm);
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, SelectedComponentTypes")] CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                Category tempCategory = new Category()
                {
                    Name = categoryVM.Name,
                };

                Category category = _context.Add(tempCategory).Entity;

                foreach (var id in categoryVM.SelectedComponentTypes)
                {
                    ComponentType componentType = _context.ComponentType.Find(long.Parse(id));

                    ComponentTypeCategory tempCtc = new ComponentTypeCategory
                    {
                        Category = category,
                        ComponentType = componentType
                    };
                    ComponentTypeCategory ctc = _context.Add(tempCtc).Entity;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            List<SelectListItem> componentTypesAsSelectList = await _context.ComponentType.Select(ct =>
                new SelectListItem()
                {
                    Value = ct.ComponentTypeId.ToString(),
                    Text = ct.ComponentName
                }
            ).ToListAsync();

            CategoryViewModel vm = new CategoryViewModel
            {
                Name = category.Name,
                ComponentTypes = new MultiSelectList(componentTypesAsSelectList.OrderBy(ct => ct.Text), "Value", "Text")
            };
            return View(vm);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name, SelectedComponentTypes")] CategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Find category including the linked ComponentTypeCategories
                Category category = _context.Category
                                            .Include(c => c.ComponentTypeCategories)
                                            .Single(c => c.CategoryId == id);

                category.Name = vm.Name;

                _context.ComponentTypeCategory.RemoveRange(category.ComponentTypeCategories);

                foreach (var sid in vm.SelectedComponentTypes)
                {
                    ComponentType componentType = _context.ComponentType.Find(long.Parse(sid));

                   // var isPresent = category.ComponentTypeCategories.FirstOrDefault(s => s.ComponentType == componentType);

                   // if (isPresent == null)
                   // {
                    var tempCtc = new ComponentTypeCategory
                    {
                        Category = category,
                        ComponentType = componentType
                    };

                    ComponentTypeCategory ctc = _context.Add(tempCtc).Entity;

                    category.ComponentTypeCategories.Add(ctc);
                    //  }
                }

                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(long id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}

