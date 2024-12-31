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
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;

namespace Ord.Hospital.Services
{
    public class ProvinceService :
         CrudAppService<
         Province,
         ProvinceDto,
         int,
         PagedAndSortedResultRequestDto,
         CreateUpdateProvinceDto>, IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IRepository<Province, int> _repository;
        private readonly IObjectMapper _objectMapper;

        public ProvinceService(IProvinceRepository provinceRepository, IObjectMapper objectMapper, IRepository<Province, int> repository
            ) : base(repository)
        {
            _provinceRepository = provinceRepository;
            _objectMapper = objectMapper;
            _repository = repository;
        }
        public override async Task<ProvinceDto> GetAsync(int id) 
        { 
            var province = await _repository.GetAsync(id); 
            if (province == null) { 
                throw new EntityNotFoundException(typeof(Province), id); 
            } 
            var mappedProvince = _objectMapper.Map<Province, ProvinceDto>(province); 

            return mappedProvince; 
        }
        public override async Task<ProvinceDto> CreateAsync(CreateUpdateProvinceDto input)
        {
            var provinceCode = await _provinceRepository.GetByCodeAsync(input.ProvinceCode);
            if (provinceCode != null)
            {
                throw new AbpValidationException("Mã tỉnh đã tồn tại",
                    new List<ValidationResult> { new ValidationResult("Mã tỉnh đã tồn tại") });
            }
            var province = _objectMapper.Map<CreateUpdateProvinceDto, Province>(input);
            await _repository.InsertAsync(province, true);
            return _objectMapper.Map<Province, ProvinceDto>(province);
        }

        public override async Task<ProvinceDto> UpdateAsync(int id, CreateUpdateProvinceDto input)
        {
            var provinceCode = await _provinceRepository.GetByCodeAsync(input.ProvinceCode);
            var provinceID = await _repository.GetAsync(id);
            if (provinceCode != null && provinceCode.Id != id)
            {
                throw new AbpValidationException("Mã tỉnh đã tồn tại",
                    new List<ValidationResult> { new ValidationResult("Mã tỉnh đã tồn tại") });
            }
            //var province = _objectMapper.Map(input, provinceID);
            //await _repository.UpdateAsync(province);
            //return _objectMapper.Map<Province, ProvinceDto>(province);
            return await base.UpdateAsync(id, input);
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
                var provinces = _objectMapper.Map< List <CreateUpdateProvinceDto> ,List <Province>>(input);
                await _repository.InsertManyAsync(provinces);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
