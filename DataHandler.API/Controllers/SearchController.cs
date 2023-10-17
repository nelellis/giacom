using CsvHelper.Configuration;
using CsvHelper;
using DataHandler.Entities;
using DataHandler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using DataHandler.ViewModels;
using DataHandler.ViewModels.Requests;
using DataHandler.ViewModels.History;
using System.ComponentModel.DataAnnotations;

namespace DataHandler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ICallDetailRecordService _callDetailRecordService;
        public SearchController(ICallDetailRecordService callDetailRecordService, IFileService fileService)
        {
            _callDetailRecordService = callDetailRecordService;
        }

        [HttpGet("recipient/{recipient}")]
        public async Task<IList<CallDetailRecordDto>> RecipientHistory([Required] string recipient, [FromQuery] RecipientHistoryRequest request)
        {
            return await _callDetailRecordService.RecipientHistory(recipient, request);
        }

        [HttpGet("caller/{callerId}")]
        public async Task<IList<CallDetailRecordDto>> CallerHistory([Required] string callerId, [FromQuery] CallerHistoryRequest request)
        {
            return await _callDetailRecordService.CallerHistory(callerId, request);
        }
    }
}
