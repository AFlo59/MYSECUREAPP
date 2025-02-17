using System.ComponentModel.DataAnnotations;

namespace MySecureApp.Models
{
    public class UserPublic : UserEntity
    {
        [Required]
        public string SubscriptionType { get; private set; } = string.Empty;  // 🔥 Correction

        private UserPublic() { }

        public UserPublic(string name, string email, string passwordHash, Role role, string subscriptionType)
            : base(name, email, passwordHash, role)
        {
            SubscriptionType = subscriptionType;
        }
    }
}
