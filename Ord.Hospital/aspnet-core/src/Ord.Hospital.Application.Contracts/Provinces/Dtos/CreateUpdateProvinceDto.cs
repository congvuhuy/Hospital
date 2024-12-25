using Ord.Hospital.AddressType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ord.Hospital.Provinces.Dtos
{
    public class CreateUpdateProvinceDto
    {
        public string ProvinceName { get; set; } = string.Empty;
        public int ProvinceCode { get; set; }
        public ProvinceType ProvinceType { get; set; }
    }
}
