using System;
using System.Collections.Generic;
using System.Text;

namespace Ord.Hospital.UserHospitals.Dtos
{
    public class CreateUpdateUserHospitalDto
    {
        public Guid UserID { get; set; }= Guid.Empty;
        public int HospitalID { get; set; }= 0;
    }
}
