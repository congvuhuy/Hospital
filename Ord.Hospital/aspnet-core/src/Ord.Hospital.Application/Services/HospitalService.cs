using Ord.Hospital.Enities;
using Ord.Hospital.HospitalOrd;
using Ord.Hospital.HospitalOrd.Dtos;
using Ord.Hospital.Irepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Ord.Hospital.Services
{
    public class HospitalService :
        CrudAppService<
            Hospitals,
            HospitalDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateHospitalDto>,
        IHospitalService
    {

        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly ICommuneRepository _communeRepository;
        public HospitalService(IRepository<Hospitals, int> repository, IProvinceRepository provinceRepository, IDistrictRepository districtRepository, ICommuneRepository communeRepository) : base(repository)
        {
            _districtRepository = districtRepository;
            _provinceRepository = provinceRepository;
            _communeRepository = communeRepository;
        }
        public override async Task<HospitalDto> CreateAsync(CreateUpdateHospitalDto input)
        {
            try
            {

                var communeCode = await _communeRepository.GetByCodeAsync(input.CommuneCode);

                if (communeCode == null)
                {
                    throw new AbpValidationException("Mã xã không tồn tại",
                        new List<ValidationResult> { new ValidationResult("Mã xã không tồn tại") });
                }
                if (input.DistrictCode != communeCode.DistrictCode)
                {
                    throw new AbpValidationException("Huyện không có xã này",
                        new List<ValidationResult> { new ValidationResult("Huyện không có xã này") });
                }
                if (input.ProvinceCode != communeCode.ProvinceCode)
                {
                    throw new AbpValidationException("Tỉnh bạn chọn không tồn tại xã này",
                        new List<ValidationResult> { new ValidationResult("Tỉnh bạn chọn không tồn tại này") });
                }

                return await base.CreateAsync(input);
            }
            catch (Exception ex)
            {
                throw new AbpValidationException(ex.Message,
                        new List<ValidationResult> { new ValidationResult(ex.Message) });
            }

        }
        public override async Task<HospitalDto> UpdateAsync(int id,CreateUpdateHospitalDto input)
        {
            try
            {

                var communeCode = await _communeRepository.GetByCodeAsync(input.CommuneCode);

                if (communeCode == null)
                {
                    throw new AbpValidationException("Mã xã không tồn tại",
                        new List<ValidationResult> { new ValidationResult("Mã xã không tồn tại") });
                }
                if (input.DistrictCode != communeCode.DistrictCode)
                {
                    throw new AbpValidationException("Huyện không có xã này",
                        new List<ValidationResult> { new ValidationResult("Huyện không có xã này") });
                }
                if (input.ProvinceCode != communeCode.ProvinceCode)
                {
                    throw new AbpValidationException("Tỉnh bạn chọn không tồn tại xã này",
                        new List<ValidationResult> { new ValidationResult("Tỉnh bạn chọn không tồn tại này") });
                }

                return await base.UpdateAsync(id,input);
            }
            catch (Exception ex)
            {
                throw new AbpValidationException(ex.Message,
                        new List<ValidationResult> { new ValidationResult(ex.Message) });
            }

        }
    }
}
