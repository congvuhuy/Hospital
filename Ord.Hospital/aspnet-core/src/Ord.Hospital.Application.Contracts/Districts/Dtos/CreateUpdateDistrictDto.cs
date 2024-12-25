using Ord.Hospital.AddressType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ord.Hospital.Districts.Dtos
{
    public class CreateUpdateDistrictDto
    {
        public string DistrictName { get; set; } = string.Empty;
        public int DistrictCode { get; set; }
        public DistrictType DistrictType { get; set; }
        public int ProvinceCode { get; set; }
    }
}
