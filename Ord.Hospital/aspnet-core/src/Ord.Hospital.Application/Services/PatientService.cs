using AutoMapper;
using Ord.Hospital.Enities;
using Ord.Hospital.HospitalOrd;
using Ord.Hospital.Irepositories;
using Ord.Hospital.Patients;
using Ord.Hospital.Patients.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;

namespace Ord.Hospital.Services
{
    public class PatientService :
        CrudAppService<
             Patient,
             PatientDto,
             int,
             PagedAndSortedResultRequestDto,
             CreateUpdatePatientDto>,
        IPatitentService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly ICommuneRepository _communeRepository;
        private readonly IHospitalService _hospitalService;
        public PatientService(IPatientRepository patientRepository, IMapper mapper, IRepository<Patient, int> repository,
            IProvinceRepository provinceRepository, IDistrictRepository districtRepository, ICommuneRepository communeRepository) : base(repository)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
            _districtRepository = districtRepository;
            _provinceRepository = provinceRepository;
            _communeRepository = communeRepository;
        }
        public async Task<PagedResultDto<PatientDto>> GetListPagingAsync(PagedAndSortedResultRequestDto input, Guid UserID)
        {
            var totalCount = await _patientRepository.GetTotalCountAsync(UserID);
            var patients = await _patientRepository.GetListByUserID(input.SkipCount, input.MaxResultCount, input.Sorting, UserID);

            var patientDtos = _mapper.Map<List<Patient>, List<PatientDto>>(patients);

            return new PagedResultDto<PatientDto>
            {
                TotalCount = totalCount,
                Items = patientDtos
            };
        }
        public override async Task<PatientDto> CreateAsync(CreateUpdatePatientDto input)
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
        public override async Task<PatientDto> UpdateAsync(int id, CreateUpdatePatientDto input)
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
