using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompatibilityWebSite.Models
{
    public class Query
    {
        public string QueryName { get; set; }
        public string ErrorName { get; set; }
        public int ErrorFlag { get; set; }
        public int DeseaseId { get; set; }
        [Required(ErrorMessage ="Поле не повинно бути порожнє")]
        [Range(1, 1000, ErrorMessage ="Поле повинно бути від 1 до 1000")]
        public int Quantity { get; set; } 
        public int ActiveSubstanceId { get; set; }
        public List<string> ActiveSubstanceNames { get; set; }
        public int CompatibilityStatusId { get; set; }
        public List <string> DeseaseIds { get; set; }
        public List <string> DeseaseNames { get; set; }
        public List<string> MedicineNames { get; set; }
        public List<string> CompatibilityStatusIds { get; set; }
    }
}
