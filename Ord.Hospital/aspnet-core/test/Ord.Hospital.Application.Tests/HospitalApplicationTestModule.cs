using Volo.Abp.Modularity;

namespace Ord.Hospital;

[DependsOn(
    typeof(HospitalApplicationModule),
    typeof(HospitalDomainTestModule)
)]
public class HospitalApplicationTestModule : AbpModule
{

}
