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
    public class ProvinceRepository : DapperRepository<HospitalDbContext>, IProvinceRepository, ITransientDependency
    {
        public ProvinceRepository(IDbContextProvider<HospitalDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
        public virtual async Task<List<Province>> GetAllAsync(int pageNumber, int pageSize)
        {
            var dbConnection = await GetDbConnectionAsync();
            var sql = @"SELECT * FROM Province ORDER BY ProvinceCode LIMIT @PageSize OFFSET @Offset;";
            var parameters = new
            {
                Offset = (pageNumber - 1) * pageSize,
                PageSize = pageSize
            };

            return (await dbConnection.QueryAsync<Province>(sql, parameters, transaction: await GetDbTransactionAsync())).ToList();
        }

        public async Task<Province> GetByCodeAsync(int code)
        {

            var dbConnection = await GetDbConnectionAsync();
            var sql = @"SELECT * FROM Province WHERE ProvinceCode=@Code";
            var parameters = new { Code = code };
            return (dbConnection.QueryFirstOrDefault<Province>(sql, parameters, transaction: await GetDbTransactionAsync()));
        }
    }
}
