using Xunit;

namespace Ord.Hospital.EntityFrameworkCore;

[CollectionDefinition(HospitalTestConsts.CollectionDefinitionName)]
public class HospitalEntityFrameworkCoreCollection : ICollectionFixture<HospitalEntityFrameworkCoreFixture>
{

}
