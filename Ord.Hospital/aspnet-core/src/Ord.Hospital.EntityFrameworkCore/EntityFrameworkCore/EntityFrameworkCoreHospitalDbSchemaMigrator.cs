using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ord.Hospital.Data;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.EntityFrameworkCore;

public class EntityFrameworkCoreHospitalDbSchemaMigrator
    : IHospitalDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreHospitalDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the HospitalDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<HospitalDbContext>()
            .Database
            .MigrateAsync();
    }
}
