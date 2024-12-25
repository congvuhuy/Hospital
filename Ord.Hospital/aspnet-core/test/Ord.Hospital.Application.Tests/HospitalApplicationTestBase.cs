using Volo.Abp.Modularity;

namespace Ord.Hospital;

public abstract class HospitalApplicationTestBase<TStartupModule> : HospitalTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
