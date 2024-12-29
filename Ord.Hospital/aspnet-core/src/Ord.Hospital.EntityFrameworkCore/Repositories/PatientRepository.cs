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
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Ord.Hospital.Repositories
{
    public class PatientRepository : DapperRepository<HospitalDbContext>, IPatientRepository
    {
        public PatientRepository(IDbContextProvider<HospitalDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
        public async Task<List<Patient>> GetListByUserID(int SkipCount, int MaxResultCount, string Sorting, Guid UserID)
        {
            try
            {
                var dbConnection = await GetDbConnectionAsync();
                var sql = $@" 
                        SELECT Patient.Id,PatientName,provinceCode,districtCode,communeCode,address,UserHospital.hospitalID
                        FROM Patient INNER JOIN UserHospital 
                        ON Patient.HospitalID=UserHospital.HospitalID
                        WHERE Patient.IsDeleted=false AND UserID=@UserID
                        ORDER BY {Sorting ?? "Patient.Id"} 
                        LIMIT @MaxResultCount 
                        OFFSET @SkipCount";

                var parameters = new { SkipCount, MaxResultCount, UserID };
                return (await dbConnection.QueryAsync<Patient>(sql, parameters)).ToList();
            }catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<int> GetTotalCountAsync(Guid userID)
        {
            var dbConnection = await GetDbConnectionAsync();
            var sql = "SELECT COUNT(*) FROM Patient " +
                    "INNER JOIN UserHospital ON Patient.HospitalID = UserHospital.HospitalID " +
                    "WHERE Patient.IsDeleted = false AND UserHospital.UserID = @UserID";
            var parameters = new { UserID = userID };
            return await dbConnection.ExecuteScalarAsync<int>(sql);
        }
    }
}
