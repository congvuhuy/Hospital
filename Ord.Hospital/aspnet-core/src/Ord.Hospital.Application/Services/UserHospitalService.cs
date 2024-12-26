using Ord.Hospital.Enities;
using Ord.Hospital.UserHospitals;
using Ord.Hospital.UserHospitals.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ord.Hospital.Services
{
    public class UserHospitalService :
        CrudAppService<
            UserHospital,
            UserHospitalDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateUserHospitalDto
            >, IUserHospitalService
    {
        public UserHospitalService(IRepository<UserHospital, int> repository) : base(repository)
        {
        }
    }
}
