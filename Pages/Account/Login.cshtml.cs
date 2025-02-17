using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations; 
using MySecureApp.Models;  // Assure-toi d’avoir cette ligne en haut de ton fichier

namespace MySecureApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty, Required]  // 🔥 Correction
        public string Email { get; set; } = string.Empty;  // ✅ Correction pour éviter null

        [BindProperty, Required]  // 🔥 Correction
        public string Password { get; set; } = string.Empty;  // ✅ Correction pour éviter null

        public void OnGet() { }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError(string.Empty, "Veuillez remplir tous les champs.");
                return Page();
            }
            return RedirectToPage("/Index");  // 🔥 Redirige vers la page d'accueil après connexion
        }
    }
}
