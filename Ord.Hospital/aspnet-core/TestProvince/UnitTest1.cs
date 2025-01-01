using Ord.Hospital.Enities;
using Ord.Hospital.Provinces.Dtos;
using Ord.Hospital.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Moq;
using Xunit;
using Ord.Hospital.Irepositories;

public class UnitTest1
{
    private readonly ProvinceService _service;
    private readonly Mock<IRepository<Province, int>> _mockRepository;
    private readonly Mock<IProvinceRepository> _mockProvinceRepo;
    private readonly Mock<IObjectMapper> _mockObjectMapper;

    public UnitTest1()
    {
        _mockRepository = new Mock<IRepository<Province, int>>();
        _mockProvinceRepo = new Mock<IProvinceRepository>();
        _mockObjectMapper = new Mock<IObjectMapper>();

        _service = new ProvinceService(_mockRepository.Object, _mockProvinceRepo.Object, _mockObjectMapper.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnProvince()
    {
        // Arrange
        int id = 0;
        var province = new Province { ProvinceCode = 1, ProvinceName = "Province1" };
        var provinceDto = new ProvinceDto { ProvinceCode = 1, ProvinceName = "Province1" };

        _mockRepository.Setup(repo => repo.GetAsync(id, true, default)).ReturnsAsync(province);
        _mockObjectMapper.Setup(m => m.Map<Province, ProvinceDto>(province)).Returns(provinceDto);

        // Act
        var result = await _service.GetAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(provinceDto, result);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateProvince()
    {
        // Arrange
        var input = new CreateUpdateProvinceDto { ProvinceCode = 1, ProvinceName = "Province2" };
        var province = new Province { ProvinceCode = 1, ProvinceName = "Province2" };
        var provinceDto = new ProvinceDto { ProvinceCode = 1, ProvinceName = "Province2" };

        _mockProvinceRepo.Setup(repo => repo.GetByCodeAsync(input.ProvinceCode)).ReturnsAsync((Province)null);
        _mockObjectMapper.Setup(m => m.Map<CreateUpdateProvinceDto, Province>(input)).Returns(province);
        _mockObjectMapper.Setup(m => m.Map<Province, ProvinceDto>(province)).Returns(provinceDto);
        _mockRepository.Setup(repo => repo.InsertAsync(province, true, default)).ReturnsAsync(province);

        // Act
        var result = await _service.CreateAsync(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(provinceDto, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProvince()
    {
        // Arrange
        int id = 0;
        var input = new CreateUpdateProvinceDto { ProvinceCode = 1, ProvinceName = "Province2" };
        var inputMappedProvince = new Province { ProvinceCode = 1, ProvinceName = "Province2" };
        var existingProvince = new Province { ProvinceCode = 1, ProvinceName = "Province1" };
        var resultDto = new ProvinceDto { ProvinceCode = 1, ProvinceName = "Province2" };

        _mockProvinceRepo.Setup(repo => repo.GetByCodeAsync(input.ProvinceCode)).ReturnsAsync(existingProvince);
        _mockRepository.Setup(repo => repo.GetAsync(id, true, default)).ReturnsAsync(existingProvince);
        _mockObjectMapper.Setup(m => m.Map<CreateUpdateProvinceDto, Province>(input)).Returns(inputMappedProvince);
        _mockObjectMapper.Setup(m => m.Map<Province, ProvinceDto>(inputMappedProvince)).Returns(resultDto);
        _mockRepository.Setup(repo => repo.UpdateAsync(inputMappedProvince, true, default)).ReturnsAsync(inputMappedProvince);

        // Act
        var result = await _service.UpdateAsync(id, input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(resultDto, result);
    }

    [Fact]
    public async Task GetListPagingAsync_ShouldReturnPagedResult()
    {
        // Arrange
        var input = new PagedAndSortedResultRequestDto { SkipCount = 0, MaxResultCount = 10, Sorting = "ProvinceName" };
        var provinces = new List<Province>
        {
            new Province { ProvinceCode = 1, ProvinceName = "Province1" },
            new Province { ProvinceCode = 2, ProvinceName = "Province2" }
        };
        var provinceDtos = new List<ProvinceDto>
        {
            new ProvinceDto { ProvinceCode = 1, ProvinceName = "Province1" },
            new ProvinceDto { ProvinceCode = 2, ProvinceName = "Province2" }
        };

        _mockProvinceRepo.Setup(repo => repo.GetTotalCountAsync()).ReturnsAsync(provinces.Count);
        _mockProvinceRepo.Setup(repo => repo.GetPagedProvincesAsync(input.SkipCount, input.MaxResultCount, input.Sorting)).ReturnsAsync(provinces);
        _mockObjectMapper.Setup(m => m.Map<List<Province>, List<ProvinceDto>>(provinces)).Returns(provinceDtos);

        // Act
        var result = await _service.GetListPagingAsync(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(provinceDtos.Count, result.TotalCount);
        Assert.Equal(provinceDtos, result.Items);
    }

    [Fact]
    public async Task GetByCode_ShouldReturnProvince()
    {
        // Arrange
        int code = 1;
        var province = new Province { ProvinceCode = 1, ProvinceName = "Province1" };
        var provinceDto = new ProvinceDto { ProvinceCode = 1, ProvinceName = "Province1" };

        _mockProvinceRepo.Setup(repo => repo.GetByCodeAsync(code)).ReturnsAsync(province);
        _mockObjectMapper.Setup(m => m.Map<Province, ProvinceDto>(province)).Returns(provinceDto);

        // Act
        var result = await _service.GetByCode(code);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(provinceDto, result);
    }

    [Fact]
    public async Task CreateMultipleAsync_ShouldCreateMultipleProvinces()
    {
        // Arrange
        var input = new List<CreateUpdateProvinceDto>
        {
            new CreateUpdateProvinceDto { ProvinceCode = 1, ProvinceName = "Province1" },
            new CreateUpdateProvinceDto { ProvinceCode = 2, ProvinceName = "Province2" }
        };
        var provinces = new List<Province>
        {
            new Province { ProvinceCode = 1, ProvinceName = "Province1" },
            new Province { ProvinceCode = 2, ProvinceName = "Province2" }
        };

        _mockObjectMapper.Setup(m => m.Map<List<CreateUpdateProvinceDto>, List<Province>>(input)).Returns(provinces);
        _mockRepository.Setup(repo => repo.InsertManyAsync(provinces, true, default)).Returns(Task.CompletedTask);

        // Act
        await _service.CreateMultipleAsync(input);

        // Assert
        _mockRepository.Verify(repo => repo.InsertManyAsync(provinces, true, default), Times.Once);
    }
}
