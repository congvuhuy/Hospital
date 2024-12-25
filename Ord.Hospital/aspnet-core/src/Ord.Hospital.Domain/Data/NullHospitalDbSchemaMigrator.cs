using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Data;

/* This is used if database provider does't define
 * IHospitalDbSchemaMigrator implementation.
 */
public class NullHospitalDbSchemaMigrator : IHospitalDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
