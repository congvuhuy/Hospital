using Ord.Hospital.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Irepositories
{
    public interface IProvinceRepository :ITransientDependency
    {
        public Task<List<Province>> GetPagedProvincesAsync(int SkipCount,int MaxResultCount, string Sorting);
        public Task<int> GetTotalCountAsync();
        Task<Province> GetByCodeAsync(int code);
    }
}
