using AutoMapper;
using Ord.Hospital.Communes;
using Ord.Hospital.Communes.Dtos;
using Ord.Hospital.Districts;
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
    public class CommuneService :
        CrudAppService<
        Commune,
        CommuneDto,
        int,
        PagedAndSortedResultRequestDto,
        CreateUpdateCommuneDto>, ICommuneService
    {
        private readonly ICommuneRepository _communeRepository;
        private readonly IMapper _mapper;
        private readonly IDistrictService _districtService;
        private readonly IProvinceService _provinceService;
        private readonly IRepository<Commune, int> _repository;
        public CommuneService(ICommuneRepository communeRepository, IMapper mapper, IProvinceService provinceService, IDistrictService districtService, IRepository<Commune, int> repository
           ) : base(repository)
        {
            _communeRepository = communeRepository;
            _mapper = mapper;
            _districtService = districtService;
            _provinceService = provinceService;
            _repository = repository;
        }

        public override async Task<CommuneDto> CreateAsync(CreateUpdateCommuneDto input)
        {
            try
            {
                var provinceCode = await _provinceService.GetByCode(input.ProvinceCode);
                var districtCode = await _districtService.GetByCode(input.DistrictCode);
                var communeCode = await _communeRepository.GetByCodeAsync(input.CommuneCode);
                if (communeCode != null)
                {
                    throw new AbpValidationException("Mã xã đã tồn tại",
                          new List<ValidationResult> { new ValidationResult("Mã xã đã tồn tại") });
                }
                if (provinceCode.ProvinceCode != districtCode.ProvinceCode)
                {
                    throw new AbpValidationException("Tỉnh bạn chọn không tồn tại huyện này",
                        new List<ValidationResult> { new ValidationResult("Tỉnh bạn chọn không tồn tại huyện này") });
                }
                if (districtCode==null)
                {
                    throw new AbpValidationException("Huyện bạn chọn không tồn tại",
                       new List<ValidationResult> { new ValidationResult("Huyện bạn chọn không tồn tại") });
                }

                return await base.CreateAsync(input);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        public override async Task<CommuneDto> UpdateAsync(int id,CreateUpdateCommuneDto input)
        {
            var provinceCode = await _provinceService.GetByCode(input.ProvinceCode);
            var districtCode = await _districtService.GetByCode(input.DistrictCode);
            var communeCode = await _communeRepository.GetByCodeAsync(input.CommuneCode);
            var communeId = _repository.GetAsync(id);
            if (communeCode != null && communeCode.Id!=id)
            {
                throw new AbpValidationException("Mã xã đã tồn tại",
                      new List<ValidationResult> { new ValidationResult("Mã xã đã tồn tại") });
            }
            if (provinceCode.ProvinceCode != districtCode.ProvinceCode)
            {
                throw new AbpValidationException("Tỉnh bạn chọn không tồn tại huyện này",
                    new List<ValidationResult> { new ValidationResult("Tỉnh bạn chọn không tồn tại huyện này") });
            }
            if (districtCode == null)
            {
                throw new AbpValidationException("Huyện bạn chọn không tồn tại",
                   new List<ValidationResult> { new ValidationResult("Huyện bạn chọn không tồn tại") });
            }

            return await base.UpdateAsync(id,input);
        }
        public async Task CreateMultipleAsync(List<CreateUpdateCommuneDto> communeList)
        {
            try
            {
                var communes = _mapper.Map<List<Commune>>(communeList);
                await _repository.InsertManyAsync(communes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        public async Task<PagedResultDto<CommuneDto>> GetListPagingAsync(PagedAndSortedResultRequestDto input)
        {
            var totalCount = await _communeRepository.GetTotalCountAsync();
            var Communes = await _communeRepository.GetPagedCommunesAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            var communeDtos = _mapper.Map<List<Commune>, List<CommuneDto>>(Communes);

            return new PagedResultDto<CommuneDto>
            {
                TotalCount = totalCount,
                Items = communeDtos
            };
        }

        public async Task<List<CommuneDto>> getListByDistrictCode(int districtCode)
        {
            var communeByProvinceCode = await _communeRepository.GetByDistrictCodeAsync(districtCode);
            return _mapper.Map<List<CommuneDto>>(communeByProvinceCode);
        }

        public async Task<CommuneDto> GetByCode(int code)
        {
            var commune = await _communeRepository.GetByCodeAsync(code);
            return _mapper.Map<CommuneDto>(commune);
        }
    }
}
