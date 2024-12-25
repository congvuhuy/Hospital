using System;
using System.Collections.Generic;
using System.Text;

namespace Ord.Hospital.HospitalOrd.Dtos
{
    public class CreateUpdateHospitalDto
    {
        public string HospitalName { get; set; }
        public int ProvinceCode { get; set; }
        public int DistrictCode { get; set; }
        public int CommuneCode { get; set; }
        public string Address { get; set; }
    }
}
