using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAssignment3.Models;

namespace WebAssignment3.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            ComponentTypes = new List<ComponentTypeViewModel>();
            Components = new List<ComponentViewModel>();
        }

        [Display(Name = "Categories")]
        public SelectList CategoriesSelect { get; set; }

        [Display(Name ="Component Types")]
        public SelectList ComponentTypesSelect { get; set; }

        public long SelectedCategoryId { get; set; }

        public long SelectedComponentTypeId { get; set; }

        [Display(Name ="Component Types")]
        public List<ComponentTypeViewModel> ComponentTypes { get; set; }

        [Display(Name = "Components")]
        public List<ComponentViewModel> Components { get; set; }
    }
}
