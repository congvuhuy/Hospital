using Ord.Hospital.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ord.Hospital.Irepositories
{
    public interface IDistrictRepository
    {
        Task<List<District>> GetAllAsync(int pageNumber, int pageSize);
        Task<District> GetByCodeAsync(int code);
    }
}
