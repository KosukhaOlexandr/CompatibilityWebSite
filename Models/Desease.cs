using System;
using System.Collections.Generic;

#nullable disable
using System.ComponentModel.DataAnnotations;

namespace CompatibilityWebSite
{
    public partial class Desease
    {
        public Desease()
        {
            DeseasesActiveSubstances = new HashSet<DeseasesActiveSubstance>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле має бути заповнене")]
        [Display(Name = "Назва")]
        [MaxLength(100, ErrorMessage = "Поле має бути менше ніж 100 символів")]
        public string Name { get; set; }
        [Display(Name = "Інформація")]
        [MaxLength(1000, ErrorMessage = "Поле має бути менше ніж 1000 символів")]
        public string Info { get; set; }

        public virtual ICollection<DeseasesActiveSubstance> DeseasesActiveSubstances { get; set; }
    }
}
