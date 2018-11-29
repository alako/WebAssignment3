using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAssignment3.Data;
using WebAssignment3.Models;
using WebAssignment3.ViewModels;

namespace WebAssignment3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel
            {
                CategoriesSelect = new SelectList(_context.Category.ToList(), "CategoryId", "Name"),
                ComponentTypesSelect = new SelectList(_context.ComponentType.ToList(), "ComponentTypeId", "ComponentName")

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel vm)
        {
            if (vm.SelectedCategoryId != 0)
            {
                var componentTypeCatsFromDb = _context.ComponentTypeCategory.Where(ctc => ctc.CategoryId == vm.SelectedCategoryId).ToList();

                foreach (var ctc in componentTypeCatsFromDb)
                {
                    var tempComponentType = _mapper.Map<ComponentTypeViewModel>(_context.ComponentType.Find(ctc.ComponentTypeId));
                    vm.ComponentTypes.Add(tempComponentType);
                }
            }

            if (vm.SelectedComponentTypeId != 0)
            {
                var componentTypeFromDb = _context.ComponentType.Find(vm.SelectedComponentTypeId);

                foreach(var component in componentTypeFromDb.Components)
                {
                    vm.Components.Add(_mapper.Map<ComponentViewModel>(component));
                }
            }

            vm.CategoriesSelect = new SelectList(_context.Category.ToList(), "CategoryId", "Name");
            vm.ComponentTypesSelect= new SelectList(_context.ComponentType.ToList(), "ComponentTypeId", "ComponentName");
            
            return View(vm);
        }
    }   
}
