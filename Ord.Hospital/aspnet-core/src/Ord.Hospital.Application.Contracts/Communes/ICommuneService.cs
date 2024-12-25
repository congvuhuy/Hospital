using Ord.Hospital.Communes.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Communes
{
    public interface ICommuneService : IScopedDependency
    {
        Task CreateMultipleAsync(List<CreateUpdateCommuneDto> communeList);
        public Task<List<CommuneDto>> GetFilterAsync(int pageNumber, int pageSize);
    }
}
