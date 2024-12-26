using AutoMapper;
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
using Volo.Abp.Domain.Repositories;
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
        private readonly IMapper _mapper;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IRepository<Province, int> _repository;


        public ProvinceService(IProvinceRepository provinceRepository, IMapper mapper, IRepository<Province, int> repository
            ) : base(repository)
        {
            _provinceRepository = provinceRepository;
            _mapper = mapper;
            _repository = repository;
        }
        public override async Task<ProvinceDto> CreateAsync(CreateUpdateProvinceDto input)
        { 
            var provinceCode=_provinceRepository.GetByCodeAsync(input.ProvinceCode);
            if (provinceCode != null)
            {
                throw new AbpValidationException("Mã tỉnh đã tồn tại",
                    new List<ValidationResult> { new ValidationResult("Mã tỉnh đã tồn tại") });
            }
            return await base.CreateAsync(input);
        }
        public override async Task<ProvinceDto> UpdateAsync(int id,CreateUpdateProvinceDto input)
        {
            var provinceCode = _provinceRepository.GetByCodeAsync(input.ProvinceCode);
            var provinceID=_repository.GetAsync(id);
            if (provinceCode != null && provinceCode.Id!=id)
            {
                throw new AbpValidationException("Mã tỉnh đã tồn tại",
                    new List<ValidationResult> { new ValidationResult("Mã tỉnh đã tồn tại") });
            }
            return await base.UpdateAsync(id,input);
        }
        public async Task<PagedResultDto<ProvinceDto>> GetListPagingAsync(PagedAndSortedResultRequestDto input)
        {
                var totalCount = await _provinceRepository.GetTotalCountAsync();
            var provinces = await _provinceRepository.GetPagedProvincesAsync(input.SkipCount,input.MaxResultCount,input.Sorting);

            var provinceDtos = _mapper.Map<List<Province>, List<ProvinceDto>>(provinces);

            return new PagedResultDto<ProvinceDto>
            {
                TotalCount = totalCount,
                Items = provinceDtos
            };
        }
        public async Task<ProvinceDto> GetByCode(int code)
        {
            var province = await _provinceRepository.GetByCodeAsync(code);
            return _mapper.Map<ProvinceDto>(province);
        }
        public async Task CreateMultipleAsync(List<CreateUpdateProvinceDto> input)
        {
            try
            {
                var provinces = _mapper.Map<List<Province>>(input);
                await _repository.InsertManyAsync(provinces);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
