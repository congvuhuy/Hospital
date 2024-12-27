using Ord.Hospital.Patients.Dtos;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Patients
{
    public interface IPatitentService:IScopedDependency
    {
        public Task<PagedResultDto<PatientDto>> GetListPagingAsync(PagedAndSortedResultRequestDto input,Guid UserID);
    }
}
