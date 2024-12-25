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
    [Table("Hospital")]
    public class Hospitals : FullAuditedEntity<int>
    {
        [Required]
        [StringLength(100)]
        public string HospitalName { get; set; }
        [Required]
        public int ProvinceCode { get; set; }
        [Required]
        public int DistrictCode { get; set; }
        [Required]
        public int CommuneCode { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
    }
}
