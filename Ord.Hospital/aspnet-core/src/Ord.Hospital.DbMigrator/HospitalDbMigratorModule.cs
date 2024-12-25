using Ord.Hospital.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Ord.Hospital.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HospitalEntityFrameworkCoreModule),
    typeof(HospitalApplicationContractsModule)
    )]
public class HospitalDbMigratorModule : AbpModule
{
}
