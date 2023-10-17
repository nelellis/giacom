using CsvHelper.Configuration;
using CsvHelper;
using DataHandler.Entities;
using DataHandler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using DataHandler.ViewModels.Stats.Requests;
using DataHandler.ViewModels.Stats.Responses;
using DataHandler.ViewModels;

namespace DataHandler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IStatsService _statsService;
        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet("average-call-cost")]
        public async Task<AverageCallCostResponse> GetAverageCallCost([FromQuery] AverageCallCostRequest request)
        {
            return await _statsService.GetAverageCallCost(request);
            
        }

        [HttpGet("average-per-day-call-count")]
        public async Task<AverageCallCountResponse> GetAveragePerDayCallCount([FromQuery] AveragePerDayCallCountRequest request)
        {
            return await _statsService.GetAveragePerDayCallCount(request);
        }

        [HttpGet("longest-calls")]
        public async Task<IList<CallDetailRecordDto>> GetLongestCalls([FromQuery] LongestCallsRequest request)
        {
            return await _statsService.GetLongestCalls(request);
        }

    }
}
