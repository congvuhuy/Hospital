using OfficeOpenXml;
using Ord.Hospital.AddressType;
using Ord.Hospital.Communes;
using Ord.Hospital.Communes.Dtos;
using Ord.Hospital.Districts;
using Ord.Hospital.Districts.Dtos;
using Ord.Hospital.Excels;
using Ord.Hospital.Irepositories;
using Ord.Hospital.Provinces;
using Ord.Hospital.Provinces.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ord.Hospital.Services
{
    public class ExcelImportService : IExcelService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IProvinceService _provinceService;
    private readonly IDistrictService _districtService;
    private readonly ICommuneService _communeService;
    public ExcelImportService(IProvinceService provinceService, IDistrictService districtService, ICommuneService communeService)
    {
        _provinceService = provinceService;
        _districtService = districtService;
        _communeService = communeService;
    }
    public async Task ImportExcelProvince(Stream excelStream)
    {
        var provinceList = new List<CreateUpdateProvinceDto>();
        var existingCodes = new HashSet<int>();
        try
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(excelStream))
            {
                var worksheet = package.Workbook.Worksheets[0];

                for (int row = 2; row <= worksheet.Dimension.End.Row - 1; row++)
                {
                    var provinceCode = int.Parse(worksheet.Cells[row, 1].Text);
                    var provincebyCode= await _provinceService.GetByCode(provinceCode);
                        if (provincebyCode != null) { 
                            throw new Exception($"Mã tỉnh {provinceCode} dòng {row} đã tồn tại.");
                        }
                    if (existingCodes.Contains(provinceCode)) { 
                            throw new Exception($"Mã tỉnh {provinceCode} dòng {row} bị lặp lại trong file."); 
                        } 
                    existingCodes.Add(provinceCode);
                    var provinceTypeStr = worksheet.Cells[row, 4].Text;
                    var normalizedProvinceTypeStr = NormalizeString(provinceTypeStr);
                    Enum.TryParse(normalizedProvinceTypeStr, out ProvinceType provinceType);
                    var createProvinceDto = new CreateUpdateProvinceDto
                    {
                        ProvinceCode = provinceCode,
                        ProvinceName = worksheet.Cells[row, 2].Text,
                        ProvinceType = provinceType,
                    };
                    provinceList.Add(createProvinceDto);
                }

                await _provinceService.CreateMultipleAsync(provinceList);

            }

        }
        catch (Exception ex)
        {
            throw new Exception("", ex);
        }

    }
    //Import District
    public async Task ImportExcelDistrict(Stream excelStream)
    {
        var districtList = new List<CreateUpdateDistrictDto>();
        var existingCodes = new HashSet<int>();
        try
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(excelStream))
            {
                var worksheet = package.Workbook.Worksheets[0];

                for (int row = 2; row <= worksheet.Dimension.End.Row - 1; row++)
                {

                    var districtCode = int.Parse(worksheet.Cells[row, 1].Text);
                    var districtbyCode= await _districtService.GetByCode(districtCode);
                        if (districtbyCode != null) { 
                            throw new Exception($"Mã huyện {districtCode} dòng {row} đã tồn tại.");
                        }
                    if (existingCodes.Contains(districtCode)) { 
                            throw new Exception($"Mã huyện {districtCode} dòng {row} bị lặp lại trong file."); 
                        } 
                    existingCodes.Add(districtCode);
                    var districtTypeStr = worksheet.Cells[row, 4].Text;
                    var normalizedDistrictTypeStr = NormalizeString(districtTypeStr);
                    Enum.TryParse(normalizedDistrictTypeStr, out DistrictType districtType);
                    var createDistrictDto = new CreateUpdateDistrictDto
                    {
                        DistrictCode = districtCode,
                        DistrictName = worksheet.Cells[row, 2].Text,
                        DistrictType = districtType,
                        ProvinceCode = int.Parse(worksheet.Cells[row, 5].Text),
                    };
                    districtList.Add(createDistrictDto);
                }

                await _districtService.CreateMultipleAsync(districtList);
            }

        }
        catch (Exception ex)
        {

            throw;
        }

    }
    //Import Commune
    public async Task ImportExcelCommune(Stream excelStream)
    {
        var communeList = new List<CreateUpdateCommuneDto>();
        var existingCodes = new HashSet<int>();

        try
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(excelStream))
            {
                 
                var worksheet = package.Workbook.Worksheets[0];

                //for (int row = 2; row <= worksheet.Dimension.End.Row-1; row++)
                for (int row = 2; row <= 2000; row++)
                {
                        var communeCode = int.Parse(worksheet.Cells[row, 1].Text);
                        var communebyCode = await _communeService.GetByCode(communeCode);
                        if (communebyCode != null)
                        {
                            throw new Exception($"Mã xã {communeCode} dòng {row} đã tồn tại.");
                        }
                        if (existingCodes.Contains(communeCode))
                        {
                            throw new Exception($"Mã xã {communeCode} dòng {row} bị lặp lại trong file.");
                        }
                        existingCodes.Add(communeCode);
                        var communeTypeStr = worksheet.Cells[row, 4].Text;
                    var normalizedCommuneTypeStr = NormalizeString(communeTypeStr);
                    Enum.TryParse(normalizedCommuneTypeStr, out CommuneType communeType);
                    var createCommuneDto = new CreateUpdateCommuneDto
                    {
                        CommuneCode = int.Parse(worksheet.Cells[row, 1].Text),
                        CommuneName = worksheet.Cells[row, 2].Text,
                        CommuneType = communeType,
                        DistrictCode = int.Parse(worksheet.Cells[row, 5].Text),
                        ProvinceCode = int.Parse(worksheet.Cells[row, 7].Text),


                    };
                    //if (createCommuneDto.CommuneCode < 0 || createCommuneDto.CommuneCode == null)
                    //{
                    //    throw new Exception($"Mã xã dòng {row} không hợp lệ");
                    //}

                    communeList.Add(createCommuneDto);
                }

                await _communeService.CreateMultipleAsync(communeList);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public string NormalizeString(string input)
    {
        var normalizedString = input.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();
        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }
        return stringBuilder.ToString()
                            .Normalize(NormalizationForm.FormC)
                            .Replace(" ", string.Empty)
                            .ToLower();
    }


}
}
