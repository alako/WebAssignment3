using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAssignment3.Models;

namespace WebAssignment3.ViewModels
{
    public class ComponentViewModel
    {
        public long ComponentId { get; set; }

        [Display(Name = "Component Type")]
        public long ComponentTypeId { get; set; }

        [Required]
        [Display(Name = "Component Number")]
        public int ComponentNumber { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNo { get; set; }

        public ComponentStatus Status { get; set; }

        [Display(Name = "Admin comment")]
        public string AdminComment { get; set; }

        [Display(Name = "User comment")]
        public string UserComment { get; set; }

        public long? CurrentLoanInformationId { get; set; }

        public SelectList ComponentTypeIdsSelect { get; set; }
    }
}
