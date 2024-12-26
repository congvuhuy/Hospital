using Dapper;
using Ord.Hospital.Enities;
using Ord.Hospital.EntityFrameworkCore;
using Ord.Hospital.Irepositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;


namespace Ord.Hospital.Repositories
{
    public class ProvinceRepository : DapperRepository<HospitalDbContext>, IProvinceRepository, ITransientDependency
    {
        public ProvinceRepository(IDbContextProvider<HospitalDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
        public async Task<List<Province>> GetPagedProvincesAsync(int SkipCount, int MaxResultCount, string Sorting)
        {
            {
                    var dbConnection = await GetDbConnectionAsync();
                    var sql = $@" 
                        SELECT * 
                        FROM Province
                        WHERE IsDeleted=false
                        ORDER BY {Sorting ?? "Id"} 
                        LIMIT @MaxResultCount 
                        OFFSET @SkipCount";
                        
                    var parameters = new { SkipCount, MaxResultCount };
                    return (await dbConnection.QueryAsync<Province>(sql, parameters)).ToList();
           
            }
        }


        public async Task<int> GetTotalCountAsync()
        {
            var dbConnection = await GetDbConnectionAsync();
                var sql = "SELECT COUNT(1) FROM Province";
                return await dbConnection.ExecuteScalarAsync<int>(sql);
        }

        public async Task<Province> GetByCodeAsync(int code)
        {
            var dbConnection = await GetDbConnectionAsync();
            var sql = @"SELECT * FROM Province WHERE ProvinceCode=@Code";
            var parameters = new { Code = code };
            return  (dbConnection.QueryFirstOrDefault<Province>(sql, parameters, transaction: await GetDbTransactionAsync()));
        }

    }
}
