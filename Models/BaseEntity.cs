using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MySecureApp.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // 🔥 Ajout d'un constructeur protégé pour l'accès par EF Core
        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }

        // 🔥 Méthode pour mettre à jour UpdatedAt
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
