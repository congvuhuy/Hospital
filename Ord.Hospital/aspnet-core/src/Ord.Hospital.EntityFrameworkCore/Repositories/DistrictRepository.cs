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
    public class DistrictRepository : DapperRepository<HospitalDbContext>, IDistrictRepository, ITransientDependency
    {
        public DistrictRepository(IDbContextProvider<HospitalDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
        public async Task<List<District>> GetPagedDistrictsAsync(int SkipCount, int MaxResultCount, string Sorting)
        {
            {
                var dbConnection = await GetDbConnectionAsync();
                var sql = $@" 
                        SELECT * 
                        FROM District
                        WHERE IsDeleted=false
                        ORDER BY {Sorting ?? "Id"} 
                        LIMIT @MaxResultCount 
                        OFFSET @SkipCount";

                var parameters = new { SkipCount, MaxResultCount };
                return (await dbConnection.QueryAsync<District>(sql, parameters)).ToList();

            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            var dbConnection = await GetDbConnectionAsync();
            var sql = "SELECT COUNT(1) FROM District";
            return await dbConnection.ExecuteScalarAsync<int>(sql);
        }

        public async Task<District> GetByCodeAsync(int code)
        {
            var dbConnection = await GetDbConnectionAsync();
            var sql = @"SELECT * FROM District WHERE DistrictCode=@Code";
            var parameters = new { Code = code };
            return (dbConnection.QueryFirstOrDefault<District>(sql, parameters, transaction: await GetDbTransactionAsync()));
        }

        public async Task<List<District>> GetByProvinceCodeAsync(int ProvinceCode)
        {
            var dbConnection = await GetDbConnectionAsync();
            var sql = @"SELECT * FROM District WHERE ProvinceCode=@provinceCode";
            var parameters = new { provinceCode = ProvinceCode };
            return (await dbConnection.QueryAsync<District>(sql, parameters, transaction: await GetDbTransactionAsync())).ToList();
        }
    }
}
