using Ord.Hospital.Samples;
using Xunit;

namespace Ord.Hospital.EntityFrameworkCore.Applications;

[Collection(HospitalTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<HospitalEntityFrameworkCoreTestModule>
{

}
