using Ord.Hospital.Provinces.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Provinces
{
    public interface IProvinceService : IScopedDependency
    {
        public Task<ProvinceDto> GetByCode(int code);
        public Task<List<ProvinceDto>> GetFilterAll(int pageNumber, int pageSize);

        public Task CreateMultipleAsync(List<CreateUpdateProvinceDto> input);
    }
}
