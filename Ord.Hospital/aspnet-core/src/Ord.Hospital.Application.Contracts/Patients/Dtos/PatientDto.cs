using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Ord.Hospital.Patients.Dtos
{
    public class PatientDto:IEntityDto<int>
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int ProvinceCode { get; set; }
        public int DistrictCode { get; set; }
        public int CommuneCode { get; set; }
        public string Address { get; set; }
        public int HospitalID { get; set; }
    }
}
