using Ord.Hospital.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Irepositories
{
    public interface ICommuneRepository : IScopedDependency
    {

        public Task<List<Commune>> GetPagedCommunesAsync(int SkipCount, int MaxResultCount, string Sorting);
        public Task<int> GetTotalCountAsync();
        Task<Commune> GetByCodeAsync(int code);
        Task<List<Commune>> GetByDistrictCodeAsync(int DistrictCode);
    }
}
