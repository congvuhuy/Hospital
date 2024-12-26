using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ord.Hospital.Enities
{
    [Table("Patient")]
    public class Patient : FullAuditedEntity<int>
    {
        [Required]
        [StringLength(70)]
        public string PatientName { get; set; }
        [Required]
        public int ProvinceCode { get; set; }
        [Required]
        public int DistrictCode { get; set; }
        [Required]
        public int CommuneCode { get; set; }
        [Required]
        public string Address { get; set; }
        public int HospitalID { get; set; }
    }
}
