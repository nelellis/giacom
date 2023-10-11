using CsvHelper.Configuration;
using CsvHelper;
using DataHandler.Entities;
using DataHandler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DataHandler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataImportController : ControllerBase
    {
        private readonly ICallDetailRecordService _callDetailRecordService;
        private readonly IFileService _fileService;
        public DataImportController(ICallDetailRecordService callDetailRecordService, IFileService fileService)
        {
            _callDetailRecordService = callDetailRecordService;
            _fileService = fileService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            var filePath = await _fileService.UploadFile(file);
            try
            {
                var message = await _callDetailRecordService.ImportFile(filePath);
                return Ok(message);
            }
            finally
            {
                _fileService.DeleteFile(filePath);
            }
            
        }

    }
}
