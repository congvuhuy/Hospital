using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Ord.Hospital.AddressType;

namespace Ord.Hospital.Enities
{
    [Table("Province")]
    public class Province : FullAuditedEntity<int>
    {
        [Required]
        [StringLength(50)]
        public string ProvinceName { get; set; }
        [Required]
        public int ProvinceCode { get; set; }
        public ProvinceType ProvinceType { get; set; }
    }
}
