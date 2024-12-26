using Dapper;
using Ord.Hospital.Enities;
using Ord.Hospital.EntityFrameworkCore;
using Ord.Hospital.Irepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Ord.Hospital.Repositories
{
    public class CommuneRepository : DapperRepository<HospitalDbContext>, ICommuneRepository, ITransientDependency
    {
        public CommuneRepository(IDbContextProvider<HospitalDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Commune>> GetPagedCommunesAsync(int SkipCount, int MaxResultCount, string Sorting)
        {
            {
                var dbConnection = await GetDbConnectionAsync();
                var sql = $@" 
                        SELECT * 
                        FROM Commune
                        WHERE IsDeleted=false
                        ORDER BY {Sorting ?? "Id"} 
                        LIMIT @MaxResultCount 
                        OFFSET @SkipCount";

                var parameters = new { SkipCount, MaxResultCount };
                return (await dbConnection.QueryAsync<Commune>(sql, parameters)).ToList();

            }
        }
        public async Task<int> GetTotalCountAsync()
        {
            var dbConnection = await GetDbConnectionAsync();
            var sql = "SELECT COUNT(1) FROM Commune";
            return await dbConnection.ExecuteScalarAsync<int>(sql);
        }

        public async Task<Commune> GetByCodeAsync(int code)
        {
            var dbConnection = await GetDbConnectionAsync();
            var sql = @"SELECT * FROM Commune WHERE CommuneCode=@Code";
            var parameters = new { Code = code };
            return (await dbConnection.QueryFirstOrDefaultAsync<Commune>(sql, parameters, transaction: await GetDbTransactionAsync()));
        }

        public async Task<List<Commune>> GetByDistrictCodeAsync(int DistrictCode)
        {
            var dbConnection = await GetDbConnectionAsync();
            var sql = @"SELECT * FROM Commune WHERE DistrictCode=@districtCode";
            var parameters = new { districtCode = DistrictCode };
            return (await dbConnection.QueryAsync<Commune>(sql, parameters, transaction: await GetDbTransactionAsync())).ToList();
        }
    }
}
