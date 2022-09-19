using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.DTOs
{
    public class ApplicationUserDTO : IdentityUser
    {
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
    }
}

