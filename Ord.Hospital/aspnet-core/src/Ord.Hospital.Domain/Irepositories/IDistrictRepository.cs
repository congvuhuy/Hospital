using Ord.Hospital.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Irepositories
{
    public interface IDistrictRepository
    {
        public Task<List<District>> GetPagedDistrictsAsync(int SkipCount, int MaxResultCount, string Sorting);
        public Task<int> GetTotalCountAsync();
        Task<District> GetByCodeAsync(int code);
        Task<List<District>> GetByProvinceCodeAsync(int ProvinceCode);
    }
}
