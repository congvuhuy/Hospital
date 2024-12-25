using Volo.Abp.Modularity;

namespace Ord.Hospital;

[DependsOn(
    typeof(HospitalDomainModule),
    typeof(HospitalTestBaseModule)
)]
public class HospitalDomainTestModule : AbpModule
{

}
