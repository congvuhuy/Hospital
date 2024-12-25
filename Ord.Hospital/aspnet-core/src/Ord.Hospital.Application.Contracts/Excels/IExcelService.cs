using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ord.Hospital.Excels
{
    public interface IExcelService:IScopedDependency
    {
        public Task ImportExcelProvince(Stream excelStream);
        public Task ImportExcelDistrict(Stream excelStream);
        public Task ImportExcelCommune(Stream excelStream);
    }

}
