using Ord.Hospital.Patients.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Patients
{
    public interface IPatitentService:IScopedDependency
    {
        public Task<List<PatientDto>> GetByHospitalID(int HospitalID);
    }
}
