using Ord.Hospital.Districts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Districts
{
    public interface IDistrictService : IScopedDependency
    {
        public Task<PagedResultDto<DistrictDto>> GetListPagingAsync(PagedAndSortedResultRequestDto input);
        public Task<DistrictDto> GetByCode(int code);
        Task CreateMultipleAsync(List<CreateUpdateDistrictDto> districtList);
        public Task<List<DistrictDto>> getListByProvinceCode(int provinceCode);

    }
}
