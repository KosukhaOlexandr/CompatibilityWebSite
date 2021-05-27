using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CompatibilityWebSite.Models;

namespace CompatibilityWebSite
{
    public class MedicineActiveSubstance
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле має бути заповнене")]
        [Display(Name = "Лікарський засіб")]
        public int MedicineId { get; set; }
        [Required(ErrorMessage = "Поле має бути заповнене")]
        [Display(Name = "Діюча речовина")]
        public int ActiveSubtanceId { get; set; }

        [Display(Name = "Лікарський засіб")]
        public virtual Medicine Medicine { get; set; }
        [Display(Name = "Діюча речовина")]
        public virtual ActiveSubstance ActiveSubstance { get; set; }
    }
}
