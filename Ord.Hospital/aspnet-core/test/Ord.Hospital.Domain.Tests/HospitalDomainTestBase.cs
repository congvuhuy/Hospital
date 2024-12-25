using Volo.Abp.Modularity;

namespace Ord.Hospital;

/* Inherit from this class for your domain layer tests. */
public abstract class HospitalDomainTestBase<TStartupModule> : HospitalTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
