using Ord.Hospital.Samples;
using Xunit;

namespace Ord.Hospital.EntityFrameworkCore.Domains;

[Collection(HospitalTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<HospitalEntityFrameworkCoreTestModule>
{

}
