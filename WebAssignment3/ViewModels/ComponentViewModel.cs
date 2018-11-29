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
        public long ComponentTypeId { get; set; }

        [Required]
        public int ComponentNumber { get; set; }
        public string SerialNo { get; set; }
        public ComponentStatus Status { get; set; }
        public string AdminComment { get; set; }
        public string UserComment { get; set; }
        public long? CurrentLoanInformationId { get; set; }
    }
}
