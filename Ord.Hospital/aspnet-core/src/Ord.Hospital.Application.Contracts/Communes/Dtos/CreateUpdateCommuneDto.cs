using Ord.Hospital.AddressType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ord.Hospital.Communes.Dtos
{
    public class CreateUpdateCommuneDto
    {
        public string CommuneName { get; set; } = string.Empty;
        public int CommuneCode { get; set; }
        public CommuneType CommuneType { get; set; }
        public int ProvinceCode { get; set; }
        public int DistrictCode { get; set; }
    }
}
