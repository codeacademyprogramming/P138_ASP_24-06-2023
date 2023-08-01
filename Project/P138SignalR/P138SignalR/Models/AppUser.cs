using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace P138SignalR.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        public string? FullName { get; set; }
    }
}
