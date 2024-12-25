using AutoMapper;
using Ord.Hospital.Enities;
using Ord.Hospital.Irepositories;
using Ord.Hospital.Provinces;
using Ord.Hospital.Provinces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
        public async Task<List<ProvinceDto>> GetFilterAll(int pageNumber, int pageSize)
        {
            var provinceList = await _provinceRepository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<List<Province>, List<ProvinceDto>>(provinceList);
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
