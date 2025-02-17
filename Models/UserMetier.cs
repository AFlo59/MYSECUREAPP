using System.ComponentModel.DataAnnotations;

namespace MySecureApp.Models
{
    public class UserMetier : UserEntity
    {
        [Required]
        public string JobTitle { get; private set; } = string.Empty;  // 🔥 Correction

        [Required]
        public string Department { get; private set; } = string.Empty;  // 🔥 Correction

        private UserMetier() { }

        public UserMetier(string name, string email, string passwordHash, Role role, string jobTitle, string department)
            : base(name, email, passwordHash, role)
        {
            JobTitle = jobTitle;
            Department = department;
        }
    }
}
