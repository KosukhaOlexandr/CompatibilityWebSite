using System;
using System.Collections.Generic;

#nullable disable

using System.ComponentModel.DataAnnotations;

namespace CompatibilityWebSite
{
    public partial class CompatibilityStatus
    {
        public CompatibilityStatus()
        {
            Compatibilities = new HashSet<Compatibility>();
        }

        public int Id { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Поле має бути заповнене")]
        [MaxLength(100, ErrorMessage = "Поле має бути менше ніж 100 символів")]

        public string Name { get; set; }
        [Display(Name = "Опис")]
        [MaxLength(175, ErrorMessage = "Поле має бути менше ніж 175 символів")]
        public string Info { get; set; }

        public virtual ICollection<Compatibility> Compatibilities { get; set; }
    }
}
