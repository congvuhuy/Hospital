using Dapper;
using Ord.Hospital.Enities;
using Ord.Hospital.EntityFrameworkCore;
using Ord.Hospital.Irepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Ord.Hospital.Repositories
{
    public class PatientRepository : DapperRepository<HospitalDbContext>, IPatientRepository
    {
        public PatientRepository(IDbContextProvider<HospitalDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
        public async Task<List<Patient>> GetByHospitalID(int hospitalID)
        {

            var dbConnection = await GetDbConnectionAsync();
            var sql = @"SELECT * FROM Patient WHERE HospitalID=@HospitalID";
            var parameters = new { HospitalID = hospitalID };
            return (await dbConnection.QueryAsync<Patient>(sql, parameters, transaction: await GetDbTransactionAsync())).ToList();
        }
    }
}
