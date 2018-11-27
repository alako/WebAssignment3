using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAssignment3.Models
{
    public class Category
    {
        public Category()
        {
            ComponentTypeCategories = new List<ComponentTypeCategory>();
        }
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ComponentTypeCategory> ComponentTypeCategories
        {
            get; protected set;
        }
    }
}
