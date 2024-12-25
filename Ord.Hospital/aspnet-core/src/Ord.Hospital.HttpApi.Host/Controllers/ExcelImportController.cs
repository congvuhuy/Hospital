using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ord.Hospital.Services;
using System.IO;
using System.Threading.Tasks;
using System;

namespace Ord.Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelImportController : ControllerBase
    {
        private readonly ExcelImportService _excelImporterService;

        public ExcelImportController(ExcelImportService excelImporterService)
        {
            _excelImporterService = excelImporterService;
        }

        [HttpPost("importProvince")]
        public async Task<ActionResult> ImportProvinceExcel(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;
                        await _excelImporterService.ImportExcelProvince(stream);
                    }
                }
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.InnerException.Message });
            }

        }
        [HttpPost("importDistrict")]
        public async Task<ActionResult> ImportDistrictExcel(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;
                        await _excelImporterService.ImportExcelDistrict(stream);
                    }
                }
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("importCommune")]
        public async Task<ActionResult> ImportCommuneExcel(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;
                        await _excelImporterService.ImportExcelCommune(stream);
                    }
                }
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
