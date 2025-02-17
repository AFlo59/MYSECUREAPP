using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MySecureApp.Models
{
    public class Role : IdentityRole<long>
    {
        [Required]
        public new string Name { get; private set; } = string.Empty;

        private Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}