using System;
using System.Collections.Generic;
using System.Text;
using Ord.Hospital.Localization;
using Volo.Abp.Application.Services;

namespace Ord.Hospital;

/* Inherit your application services from this class.
 */
public abstract class HospitalAppService : ApplicationService
{
    protected HospitalAppService()
    {
        LocalizationResource = typeof(HospitalResource);
    }
}
