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
    public class ComponentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComponentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComponentType
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComponentType.ToListAsync());
        }

        // GET: ComponentType/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentType
                .FirstOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // GET: ComponentType/Create
        public async Task<IActionResult> Create()
        {

            var categoriesAsSelectList = await _context.Category.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            }).ToListAsync();

            ComponentTypeViewModel vm = new ComponentTypeViewModel
            {
                MultiSelectCategories = new MultiSelectList(categoriesAsSelectList.OrderBy(c => c.Text), "Value", "Text"),
            };
            return View(vm);
        }

        // POST: ComponentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SelectedCategories, ComponentName,ComponentInfo,Location,Status,Datasheet,ImageUrl,Manufacturer,WikiLink,AdminComment")] ComponentTypeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                ComponentType tempComponentType = new ComponentType()
                {
                    ComponentName = vm.ComponentName,
                    AdminComment = vm.AdminComment,
                    ComponentInfo = vm.ComponentInfo,
                    Datasheet = vm.Datasheet,
                    Location = vm.Location,
                    WikiLink = vm.WikiLink,
                    Status = vm.Status,
                    Manufacturer = vm.Manufacturer,
                    ImageUrl = vm.ImageUrl,
                    Image = vm.Image,
                };

                try
                {
                    var componentType = _context.Add(tempComponentType).Entity;

                    foreach (var id in vm.SelectedCategories)
                    {
                        Category cat = _context.Category.Find(long.Parse(id));

                        if (cat != null)
                        {
                            ComponentTypeCategory ctc = new ComponentTypeCategory
                            {
                                Category = cat,
                                ComponentType = componentType,
                            };
                            _context.Add(ctc);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: ComponentType/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentType.FindAsync(id);

            if (componentType == null)
            {
                return NotFound();
            }

            var categoriesAsSelectList = await _context.Category.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            }).ToListAsync();

            ComponentTypeViewModel vm = new ComponentTypeViewModel
            {
                ComponentName = componentType.ComponentName,
                AdminComment = componentType.AdminComment,
                ComponentInfo = componentType.ComponentInfo,
                Datasheet = componentType.Datasheet,
                Location = componentType.Location,
                WikiLink = componentType.WikiLink,
                Status = componentType.Status,
                Manufacturer = componentType.Manufacturer,
                ImageUrl = componentType.ImageUrl,
                Image = componentType.Image,
                MultiSelectCategories = new MultiSelectList(categoriesAsSelectList.OrderBy(c => c.Text), "Value", "Text"),
                ComponentTypeId = componentType.ComponentTypeId,
            };


            return View(vm);
        }

        // POST: ComponentType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ComponentTypeId,ComponentName,ComponentInfo,Location,Status,Datasheet,ImageUrl,Manufacturer,WikiLink,AdminComment")] ComponentType componentType)
        {
            if (id != componentType.ComponentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(componentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentTypeExists(componentType.ComponentTypeId))
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
            return View(componentType);
        }

        // GET: ComponentType/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentType
                .FirstOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // POST: ComponentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var componentType = await _context.ComponentType.FindAsync(id);
            _context.ComponentType.Remove(componentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentTypeExists(long id)
        {
            return _context.ComponentType.Any(e => e.ComponentTypeId == id);
        }
    }
}