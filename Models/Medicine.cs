using CompatibilityWebSite.Models;
using System;
using System.Collections.Generic;
#nullable disable
using System.ComponentModel.DataAnnotations;
namespace CompatibilityWebSite
{
    public partial class Medicine
    {
        public Medicine()
        {
            MedicineActiveSubstances = new HashSet<MedicineActiveSubstance>();
        }
        public int Id { get; set; }
        [Display(Name= "Назва")]
        [Required(ErrorMessage= "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Поле має бути менше ніж 100 символів")]
        public string Name { get; set; }
        [Display(Name= "Країна виготовлення")]
        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(100, ErrorMessage = "Поле має бути менше ніж 100 символів")]
        public string Country { get; set; }
        [Display(Name= "Назва компанії")]
        [Required(ErrorMessage = "Поле обов'язкове")]
        [MaxLength(150, ErrorMessage ="Поле має бути менше ніж 150 символів")]
        public string CompanyName { get; set; }
        [Display(Name= "Інструкція")]
        [MaxLength(1000, ErrorMessage = "Поле має бути менше ніж 1000 символів")]
        public string Instruction { get; set; }

        public virtual ICollection<MedicineActiveSubstance> MedicineActiveSubstances { get; set; }
    }
}
