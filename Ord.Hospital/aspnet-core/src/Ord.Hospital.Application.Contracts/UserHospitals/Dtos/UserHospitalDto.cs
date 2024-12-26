using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Ord.Hospital.UserHospitals.Dtos
{
    public class UserHospitalDto:AuditedEntityDto<int>
    {
        public Guid UserID { get; set; }
        public int HospitalID { get; set; }
    }
}
