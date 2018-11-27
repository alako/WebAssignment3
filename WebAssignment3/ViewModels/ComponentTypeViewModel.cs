using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAssignment3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAssignment3.ViewModels
{
    public class ComponentTypeViewModel
    {
        public ComponentTypeViewModel() {
            SelectedCategories = new List<string>();
        }

        public long ComponentTypeId { get; set; }

        [Required]
        public string ComponentName { get; set; }

        [Required]
        public string ComponentInfo { get; set; }

        [Required]
        public string Location { get; set; }

        public ComponentTypeStatus Status { get; set; }

        public string Datasheet { get; set; }

        public string ImageUrl { get; set; }

        public string Manufacturer { get; set; }

        public string WikiLink { get; set; }

        public string AdminComment { get; set; }

        public virtual ESImage Image { get; set; }



        public MultiSelectList MultiSelectCategories { get; set; }

        public List<string> SelectedCategories { get; set; }

        public List<Category> Categories { get; set; }


        public MultiSelectList MultiSelectListComponents { get; set; }

        public List<string> SelectedComponents { get; set; }

        public List<Component> Components { get; set; }
    }

}
