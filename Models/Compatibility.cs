using System;
using System.Collections.Generic;

#nullable disable
using System.ComponentModel.DataAnnotations;
using CompatibilityWebSite.Models;
using Microsoft.AspNetCore.Mvc;


namespace CompatibilityWebSite
{
    public partial class Compatibility : IValidatableObject
    {
        public int Id { get; set; }
        [Display(Name = "Перша активна речовина")]
        [Required(ErrorMessage = "Поле має бути заповнене")]
       
        [Remote(action: "VerifyActiveSubs", controller: "Compatibilities",
            AdditionalFields = nameof(SecondActiveSubstance), ErrorMessage = "Зв'язок між цими діючими речовинами вже існує")]
        public int FirstActiveSubstance { get; set; }
        [Display(Name = "Друга активна речовина")]
        [Required(ErrorMessage = "Поле має бути заповнене")]
        [Remote(action: "VerifyActiveSubs", controller: "Compatibilities",
            AdditionalFields = nameof(FirstActiveSubstance), ErrorMessage = "Зв'язок між цими діючими речовинами вже існує")]
        public int SecondActiveSubstance { get; set; }
        [Display(Name = "Статус взаємодії")]
        [Required(ErrorMessage = "Поле має бути заповнене")]
        public int CompatibilityStatusId { get; set; }

        [Display(Name = "Статус взаємодії")]
        public virtual CompatibilityStatus CompatibilityStatus { get; set; }
        [Display(Name = "Перша активна речовина")]
        public virtual ActiveSubstance FirstActiveSubstanceNavigation { get; set; }
        
        [Display(Name = "Друга активна речовина")]
        public virtual ActiveSubstance SecondActiveSubstanceNavigation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (this.FirstActiveSubstance == this.SecondActiveSubstance)
            {
                errors.Add(new ValidationResult("Діючі речовини повинні бути різним"));
            }
            
            return errors;
        }
    }
}
