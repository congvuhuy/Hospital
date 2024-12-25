using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Ord.Hospital.Enities
{
    [Table("UserHospital")]
    public class UserHospital : FullAuditedEntity<int>
    {
        public int UserID { get; set; }
        public int HospitalID { get; set; }
    }
}
