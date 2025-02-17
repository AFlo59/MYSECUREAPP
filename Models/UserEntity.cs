using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySecureApp.Models
{
    public abstract class UserEntity : IdentityUser<long>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; private set; } = string.Empty;

         [ForeignKey("Role")]
        public long RoleId { get; private set; }

        public virtual Role Role { get; private set; } = null!;

        protected UserEntity() { }

        protected UserEntity(string name, string email, string passwordHash, Role role)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role ?? throw new ArgumentNullException(nameof(role));
            RoleId = role.Id;
        }

    }
}
