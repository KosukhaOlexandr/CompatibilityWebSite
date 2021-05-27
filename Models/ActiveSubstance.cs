using System;
using System.Collections.Generic;
using CompatibilityWebSite.Models;

#nullable disable

using System.ComponentModel.DataAnnotations;

namespace CompatibilityWebSite
{
    public partial class ActiveSubstance
    {
        public ActiveSubstance()
        {
            CompatibilityFirstActiveSubstanceNavigations = new HashSet<Compatibility>();
            MedicineActiveSubstances = new HashSet<MedicineActiveSubstance>();
            CompatibilitySecondActiveSubstanceNavigations = new HashSet<Compatibility>();
            DeseasesActiveSubstances = new HashSet<DeseasesActiveSubstance>();
        }
        
        public int Id { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Поле має бути заповнене")]
        [MaxLength(150, ErrorMessage = "Поле має бути менше ніж 150 символів")]
        public string Name { get; set; }
        [Display(Name = "Інформація")]
        [MaxLength(1000, ErrorMessage = "Поле має бути менше ніж 1000 символів")]

        public string Info { get; set; }

        public virtual ICollection<Compatibility> CompatibilityFirstActiveSubstanceNavigations { get; set; }
        public virtual ICollection<Compatibility> CompatibilitySecondActiveSubstanceNavigations { get; set; }
        public virtual ICollection<DeseasesActiveSubstance> DeseasesActiveSubstances { get; set; }
        public virtual ICollection<MedicineActiveSubstance> MedicineActiveSubstances { get; set; }

    }
}
