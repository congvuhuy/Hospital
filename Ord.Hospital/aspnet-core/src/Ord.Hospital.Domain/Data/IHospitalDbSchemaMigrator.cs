using System.Threading.Tasks;

namespace Ord.Hospital.Data;

public interface IHospitalDbSchemaMigrator
{
    Task MigrateAsync();
}
