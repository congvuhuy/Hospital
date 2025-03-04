﻿using Ord.Hospital.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Irepositories
{
    public interface IPatientRepository:ITransientDependency
    {
        public Task<List<Patient>> GetListByUserID(int SkipCount, int MaxResultCount, string Sorting, Guid UserID);
        public Task<int> GetTotalCountAsync(Guid userID);
    }
}
