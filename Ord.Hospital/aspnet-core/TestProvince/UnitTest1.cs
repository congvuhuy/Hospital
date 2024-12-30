using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Moq;
using Ord.Hospital.Enities;
using Ord.Hospital.Irepositories;
using Ord.Hospital.Provinces;
using Ord.Hospital.Provinces.Dtos;
using Ord.Hospital.Services;
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
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<Volo.Abp.ObjectMapping.IObjectMapper> _mockObjectMapper;
        private readonly ProvinceService _service;

        public UnitTest1()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _mockProvinceRepo = _fixture.Freeze<Mock<IProvinceRepository>>();
            _mockRepository = _fixture.Freeze<Mock<IRepository<Province, int>>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _mockObjectMapper = _fixture.Freeze<Mock<Volo.Abp.ObjectMapping.IObjectMapper>>();
            _service = new ProvinceService(_mockProvinceRepo.Object, _mockMapper.Object, _mockRepository.Object);
           
            
        }

        [Fact]
        public async Task GetAsync_ShouldReturnProvince_WhenProvinceExists()
        {
            // Arrange
            var provinceId = _fixture.Create<int>();
            var expectedProvince = _fixture.Create<Province>();
            var expectedDto = _fixture.Create<ProvinceDto>();

            _mockRepository.Setup(repo => repo.GetAsync(provinceId, true, default)).ReturnsAsync(expectedProvince);
            _mockMapper.Setup(m => m.Map<Province,ProvinceDto>(expectedProvince)).Returns(expectedDto);

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
            var input = _fixture.Create<CreateUpdateProvinceDto>();
            var mappedProvince = _fixture.Create<Province>();
            var createdProvince = _fixture.Create<Province>();
            createdProvince.ProvinceCode = input.ProvinceCode;

            _mockProvinceRepo.Setup(repo => repo.GetByCodeAsync(input.ProvinceCode)).ReturnsAsync((Province)null);
            _mockMapper.Setup(m => m.Map<Province>(input)).Returns(mappedProvince);
            _mockRepository.Setup(repo => repo.InsertAsync(mappedProvince, true, default)).ReturnsAsync(createdProvince);

            // Act
            var result = await _service.CreateAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(input.ProvinceCode, result.ProvinceCode);
        }
    }
}
