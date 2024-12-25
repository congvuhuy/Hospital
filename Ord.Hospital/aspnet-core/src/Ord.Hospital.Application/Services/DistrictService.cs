using AutoMapper;
using Ord.Hospital.Districts;
using Ord.Hospital.Districts.Dtos;
using Ord.Hospital.Enities;
using Ord.Hospital.Irepositories;
using Ord.Hospital.Provinces;
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
    public class DistrictService :
        CrudAppService<
       District,
       DistrictDto,
       int,
       PagedAndSortedResultRequestDto,
       CreateUpdateDistrictDto
       >, IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;
        private readonly IProvinceService _provinceService;
        private readonly IRepository<District, int> _repository;
        public DistrictService(IDistrictRepository districtRepository, IMapper mapper, IProvinceService provinceService, IRepository<District, int> repository
            ) : base(repository)
        {
            _districtRepository = districtRepository;
            _mapper = mapper;
            _provinceService = provinceService;
            _repository = repository;
        }


        public override async Task<DistrictDto> CreateAsync(CreateUpdateDistrictDto input)
        {
            var provinceCode = await _provinceService.GetByCode(input.ProvinceCode);
            var districtCode = await _districtRepository.GetByCodeAsync(input.DistrictCode);
            if (districtCode != null)
            {
                throw new AbpValidationException("Mã huyện đã tồn tại",
                    new List<ValidationResult> { new ValidationResult("Mã huyện đã tồn tại") });

            }
            if (provinceCode != null)
            {
                throw new AbpValidationException("Tỉnh bạn chọn không tồn tại",
                    new List<ValidationResult> { new ValidationResult("Tỉnh bạn chọn không tồn tại") });
            }

            return await base.CreateAsync(input);
        }
        public override async Task<DistrictDto> UpdateAsync(int id, CreateUpdateDistrictDto input)
        {
            var provinceCode = await _provinceService.GetByCode(input.ProvinceCode);
            var districtCode = await _districtRepository.GetByCodeAsync(input.DistrictCode);
            if (districtCode != null)
            {
                throw new AbpValidationException("Huyện bạn chọn đã tồn tại",
                    new List<ValidationResult> { new ValidationResult("Huyện bạn chọn đã tồn tại") });

            }
            if (provinceCode != null)
            {
                throw new AbpValidationException("Tỉnh bạn chọn không tồn tại",
                    new List<ValidationResult> { new ValidationResult("Tỉnh bạn chọn không tồn tại") });
            }

            return await base.CreateAsync(input);
        }
        public async Task<List<DistrictDto>> GetFilterAsync(int pageNumber, int pageSize)
        {
            var districts = await _districtRepository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<List<DistrictDto>>(districts);
        }
        public async Task<List<DistrictDto>> GetByCode(int code)
        {
            var districts = await _districtRepository.GetByCodeAsync(code);
            return _mapper.Map<List<DistrictDto>>(districts);
        }

        public async Task CreateMultipleAsync(List<CreateUpdateDistrictDto> districtList)
        {
            try
            {
                var districts = _mapper.Map<List<District>>(districtList);
                await _repository.InsertManyAsync(districts);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
