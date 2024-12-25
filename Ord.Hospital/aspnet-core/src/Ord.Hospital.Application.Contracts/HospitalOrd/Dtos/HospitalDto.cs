using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Ord.Hospital.HospitalOrd.Dtos
{
    public class HospitalDto: IEntityDto<int>
    {
        public int Id { get; set; }
        public string HospitalName { get; set; }
        public int ProvinceCode { get; set; }
        public int DistrictCode { get; set; }
        public int CommuneCode { get; set; }
        public string Address { get; set; }
    }
}
