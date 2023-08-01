using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace P138Alup.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        public string? Name { get; set; }
    }
}
