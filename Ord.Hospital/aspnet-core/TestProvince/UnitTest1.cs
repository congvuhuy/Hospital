using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using Ord.Hospital.Enities;
using Ord.Hospital.Irepositories;
using Ord.Hospital.Provinces;
using Ord.Hospital.Provinces.Dtos;
using Ord.Hospital.Services;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;
using Xunit;


namespace TestProvince
{
    public class UnitTest1
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProvinceRepository> _mockProvinceRepo;
        private readonly Mock<IRepository<Province, int>> _mockRepository;
        private readonly Mock<IObjectMapper> _mockObjectMapper;
        private readonly ProvinceService _service;

        public UnitTest1()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _mockProvinceRepo = _fixture.Freeze<Mock<IProvinceRepository>>();
            _mockRepository = _fixture.Freeze<Mock<IRepository<Province, int>>>();
            //_mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _mockObjectMapper = _fixture.Freeze<Mock<IObjectMapper>>();
            _service = new ProvinceService(_mockProvinceRepo.Object,_mockObjectMapper.Object, _mockRepository.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnProvince_WhenProvinceExists()
        {
            // Arrange
            var provinceId = 0;
            var expectedProvince = new Province { ProvinceCode=1,ProvinceName="Province1"};
            var expectedDto = new ProvinceDto { ProvinceCode = 1, ProvinceName = "Province1" };

            _mockObjectMapper.Setup(m => m.Map<Province, ProvinceDto>(expectedProvince)).Returns(expectedDto);
            _mockRepository.Setup(repo => repo.GetAsync(provinceId, true, default)).ReturnsAsync(expectedProvince);

            // Act
            var result = await _service.GetAsync(provinceId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto, result);
        
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenProvinceCodeExists()
        {
            // Arrange
            var input = _fixture.Create<CreateUpdateProvinceDto>();
            var existingProvince = _fixture.Create<Province>();

            _mockProvinceRepo.Setup(repo => repo.GetByCodeAsync(input.ProvinceCode)).ReturnsAsync(existingProvince);

            // Act
            var exception = await Assert.ThrowsAsync<AbpValidationException>(() => _service.CreateAsync(input));

            // Assert
            Assert.Equal("Mã tỉnh đã tồn tại", exception.ValidationErrors[0].ErrorMessage);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateProvince_WhenProvinceCodeDoesNotExist()
        {
            // Arrange
            var input = new CreateUpdateProvinceDto { ProvinceCode = 1, ProvinceName = "Province1" };
            var inputMappedProvince = new Province { ProvinceCode = 1,ProvinceName="Province1" };
            var ResultDto = new ProvinceDto { ProvinceCode = 1,ProvinceName="Province1" };

            _mockProvinceRepo.Setup(repo => repo.GetByCodeAsync(input.ProvinceCode)).ReturnsAsync((Province)null);
            _mockObjectMapper.Setup(m => m.Map<CreateUpdateProvinceDto,Province>(input)).Returns(inputMappedProvince);
            _mockObjectMapper.Setup(m => m.Map<Province, ProvinceDto>(inputMappedProvince)).Returns(ResultDto);
            _mockRepository.Setup(repo => repo.InsertAsync(inputMappedProvince, true, default)).ReturnsAsync(inputMappedProvince);
            // Act
            var result = await _service.CreateAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResultDto, result);
        }
        [Fact]
        public async Task UpdateAsync_ShoulUpdateProvince()
        {
            int id = 0;
            var input = new CreateUpdateProvinceDto { ProvinceCode = 1, ProvinceName = "Province2" };
            var inputMappedProvince = new Province { ProvinceCode = 1, ProvinceName = "Province2" };
            var exitingProvince = new Province { ProvinceCode = 1, ProvinceName = "Province1" };
            var ResultDto = new ProvinceDto { ProvinceCode = 1, ProvinceName = "Province2" };
            _mockObjectMapper.Setup(m=>m.Map<CreateUpdateProvinceDto,Province>(input)).Returns(inputMappedProvince);
            _mockObjectMapper.Setup(m => m.Map<Province, ProvinceDto>(inputMappedProvince)).Returns(ResultDto);
            _mockProvinceRepo.Setup(repo => repo.GetByCodeAsync(input.ProvinceCode)).ReturnsAsync(exitingProvince);
            _mockRepository.Setup(repo => repo.GetAsync(id, true, default)).ReturnsAsync(exitingProvince);
            _mockRepository.Setup(repo => repo.UpdateAsync(inputMappedProvince, true, default)).ReturnsAsync(inputMappedProvince);
            var result = await _service.UpdateAsync(id, input);

            Assert.NotNull(result);
            Assert.Equal(ResultDto, result);
        }

    }
}
