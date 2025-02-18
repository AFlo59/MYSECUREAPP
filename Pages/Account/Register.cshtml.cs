using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using MySecureApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MySecureApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public RegisterModel(UserManager<UserEntity> userManager,
                             SignInManager<UserEntity> signInManager,
                             RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "Le nom est requis.")]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "L'email est requis.")]
            [EmailAddress(ErrorMessage = "Email invalide.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Le type d'abonnement est requis.")]
            public string SubscriptionType { get; set; } = string.Empty;

            [Required(ErrorMessage = "Le mot de passe est requis.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "La confirmation du mot de passe est requise.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Pour cet exemple, nous considérons que les utilisateurs s'inscrivent en tant que "Public".
            var defaultRoleName = "Public";
            var role = await _roleManager.FindByNameAsync(defaultRoleName);
            if (role == null)
            {
                role = new Role(defaultRoleName);
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Erreur lors de la création du rôle par défaut.");
                    return Page();
                }
            }

            // Création de l'utilisateur. Ici, on utilise le constructeur de UserPublic
            // Attention : dans ce constructeur, le paramètre "passwordHash" est attendu.
            // Nous lui transmettons une chaîne vide puisque c'est UserManager qui générera le hash du mot de passe.
            var user = new UserPublic(Input.Name, Input.Email, string.Empty, role, Input.SubscriptionType);

            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                // Optionnel : connexion immédiate de l'utilisateur après inscription.
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
