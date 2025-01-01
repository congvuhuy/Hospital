using Ord.Hospital.Enities;
using Ord.Hospital.Irepositories;
using Ord.Hospital.Provinces;
using Ord.Hospital.Provinces.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;

namespace Ord.Hospital.Services
{
    public class ProvinceService : CrudAppService<
        Province,
        ProvinceDto,
        int,
        PagedAndSortedResultRequestDto,
        CreateUpdateProvinceDto>, IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IObjectMapper _objectMapper;

        public ProvinceService(
            IRepository<Province, int> repository,
            IProvinceRepository provinceRepository,
            IObjectMapper objectMapper
        ) : base(repository)
        {
            _provinceRepository = provinceRepository ?? throw new ArgumentNullException(nameof(provinceRepository));
            _objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
        }

        public override async Task<ProvinceDto> GetAsync(int id)
        {
            try
            {
                if (Repository == null) throw new NullReferenceException("Repository is null");
                if (_objectMapper == null) throw new NullReferenceException("ObjectMapper is null");

                var province = await Repository.GetAsync(id);
                if (province == null)
                {
                    throw new EntityNotFoundException(typeof(Province), id);
                }

                var mappedProvince = _objectMapper.Map<Province, ProvinceDto>(province);
                if (mappedProvince == null) throw new NullReferenceException("MappedProvince is null");

                return mappedProvince;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetAsync: " + ex.Message);
                throw;
            }
        }

        public override async Task<ProvinceDto> CreateAsync(CreateUpdateProvinceDto input)
        {
            try
            {
                var provinceCode = await _provinceRepository.GetByCodeAsync(input.ProvinceCode);
                if (provinceCode != null)
                {
                    throw new AbpValidationException("Mã tỉnh đã tồn tại",
                        new List<ValidationResult> { new ValidationResult("Mã tỉnh đã tồn tại") });
                }

                var province = _objectMapper.Map<CreateUpdateProvinceDto, Province>(input);
                await Repository.InsertAsync(province, true);

                return _objectMapper.Map<Province, ProvinceDto>(province);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in CreateAsync: " + ex.Message);
                throw;
            }
        }

        public override async Task<ProvinceDto> UpdateAsync(int id, CreateUpdateProvinceDto input)
        {
            try
            {
                if (_objectMapper == null) throw new NullReferenceException("ObjectMapper is null");
                if (_provinceRepository == null) throw new NullReferenceException("ProvinceRepository is null");
                if (Repository == null) throw new NullReferenceException("Repository is null");

                var provinceCode = await _provinceRepository.GetByCodeAsync(input.ProvinceCode);
                if (provinceCode != null && provinceCode.Id != id)
                {
                    throw new AbpValidationException("Mã tỉnh đã tồn tại",
                        new List<ValidationResult> { new ValidationResult("Mã tỉnh đã tồn tại") });
                }

                var provinceID = await Repository.GetAsync(id);
                if (provinceID == null) throw new NullReferenceException("ProvinceID is null");

                // Sử dụng ObjectMapper để map dữ liệu
                provinceID= _objectMapper.Map<CreateUpdateProvinceDto, Province>(input);

                await Repository.UpdateAsync(provinceID);

                return _objectMapper.Map<Province, ProvinceDto>(provinceID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UpdateAsync: " + ex.Message);
                throw;
            }
        }

        public async Task<PagedResultDto<ProvinceDto>> GetListPagingAsync(PagedAndSortedResultRequestDto input)
        {
            var totalCount = await _provinceRepository.GetTotalCountAsync();
            var provinces = await _provinceRepository.GetPagedProvincesAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            var provinceDtos = _objectMapper.Map<List<Province>, List<ProvinceDto>>(provinces);

            return new PagedResultDto<ProvinceDto>
            {
                TotalCount = totalCount,
                Items = provinceDtos
            };
        }

        public async Task<ProvinceDto> GetByCode(int code)
        {
            var province = await _provinceRepository.GetByCodeAsync(code);
            return _objectMapper.Map<Province, ProvinceDto>(province);
        }

        public async Task CreateMultipleAsync(List<CreateUpdateProvinceDto> input)
        {
            try
            {
                var provinces = _objectMapper.Map<List<CreateUpdateProvinceDto>, List<Province>>(input);
                await Repository.InsertManyAsync(provinces, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CreateMultipleAsync: " + ex.Message, ex);
            }
        }
    }
}
