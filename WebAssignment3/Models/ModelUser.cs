using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAssignment3.Models
{
    public class ModelUser : IdentityUser
    {
        public ModelUser() : base() { }
  
        public UserRoles userRoles { get; set; }
    }
    public enum UserRoles
    {
        BasicUser,
        Admin
    }
}
