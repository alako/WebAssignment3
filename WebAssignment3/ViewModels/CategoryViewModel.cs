using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAssignment3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAssignment3.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel() {
            SelectedComponentTypes = new List<string>();
        }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        public string CategoryId { get; set; }

        public List<string> SelectedComponentTypes { get; set; }
        public MultiSelectList ComponentTypes { get; set; }
        public List<ComponentType> DisplayComponentTypes {get; set;}
    }
}