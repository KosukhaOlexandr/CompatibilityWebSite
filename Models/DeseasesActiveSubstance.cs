using System;
using System.Collections.Generic;

#nullable disable
using System.ComponentModel.DataAnnotations;

namespace CompatibilityWebSite
{
    public partial class DeseasesActiveSubstance
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле має бути заповнене")]
        [Display(Name = "Хвороба")]
        public int DeseaseId { get; set; }
        [Required(ErrorMessage = "Поле має бути заповнене")]
        [Display(Name = "Діюча речовина")]
        public int ActiveSubstanceId { get; set; }

        [Display(Name = "Діюча речовина")]
        public virtual ActiveSubstance ActiveSubstance { get; set; }
        [Display(Name = "Хвороба")]
        public virtual Desease Desease { get; set; }
    }
}
