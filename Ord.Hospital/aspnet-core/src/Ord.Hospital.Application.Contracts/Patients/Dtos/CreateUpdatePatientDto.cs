using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ord.Hospital.Patients.Dtos
{
    public class CreateUpdatePatientDto
    {
        public string PatientName { get; set; }=string.Empty;
        public int ProvinceCode { get; set; }
        public int DistrictCode { get; set; }
        public int CommuneCode { get; set; }
        public string Address { get; set; } = string.Empty;
        public int HospitalID { get; set; }
    }
}
