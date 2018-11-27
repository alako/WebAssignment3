using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAssignment3.Models
{
    public class ModelRole : IdentityRole
    {
        public ModelRole() : base(){ }
        public ModelRole(string role) : base(role) { }
    }
}
