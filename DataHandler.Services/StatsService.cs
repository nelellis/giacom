using DataHandler.CdrDbContext;
using DataHandler.Repositories;
using DataHandler.ViewModels;
using DataHandler.ViewModels.Stats.Requests;
using DataHandler.ViewModels.Stats.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.Services
{
    public interface IStatsService
    {
        Task<AverageCallCostResponse> GetAverageCallCost(AverageCallCostRequest request);
        Task<IList<CallDetailRecordDto>> GetLongestCalls(LongestCallsRequest request);
        Task<AverageCallCountResponse> GetAveragePerDayCallCount(AveragePerDayCallCountRequest request);
    }
    public class StatsService : FilterServiceBase, IStatsService
    {
        private readonly ILogger<CallDetailRecordService> _logger;
        private readonly ICallDetailRecordRepository _callDetailRecordRepo;
        public StatsService(IDbContextFactory<ApplicationDbContext> dbContextFactory,
            ILogger<CallDetailRecordService> logger,
            ICallDetailRecordRepository callDetailRecordRepo
            ): base(callDetailRecordRepo)
        {
            _logger = logger;
            _callDetailRecordRepo = callDetailRecordRepo;
        }

        public async Task<AverageCallCostResponse> GetAverageCallCost(AverageCallCostRequest request)
        {
            var query = GetQueryable(request);
            //TODO: Currency Conversion
            var avg = await query.AverageAsync(c => c.Cost);
            return new AverageCallCostResponse()
            {
                AverageCallCost = avg
            };
        }

        public async Task<AverageCallCountResponse> GetAveragePerDayCallCount(AveragePerDayCallCountRequest request)
        {

            var query = GetQueryable(request);
            
            var countStats = await query.GroupBy(e => 1).Select(c => new
            {
                Count = c.Count(),
                MinDate = c.Min(c => c.CallDate), 
                MaxDate = c.Max(c => c.CallDate)
            }
            ).FirstOrDefaultAsync();
            if(countStats == null)
                return new AverageCallCountResponse();

            var days = ((request.CallDateTo ?? countStats.MaxDate) - (request.CallDateFrom ?? countStats.MinDate)).TotalDays;
            return new AverageCallCountResponse()
            {
                AverageCallCountPerDay = countStats.Count / days,
                TotalDays = days,
            };
        }

        public async Task<IList<CallDetailRecordDto>> GetLongestCalls(LongestCallsRequest request)
        {
            var query = GetQueryable(request);
            query = query.OrderByDescending(c => c.Duration);
            return await query.Take(request.NumberOfLongestCalls)
                .Select(c => new CallDetailRecordDto
                {
                    CallDate = c.CallDate,
                    CallerId = c.CallerId,
                    Cost = c.Cost,
                    Currency = c.Currency,
                    Duration= c.Duration,
                    EndTime= c.EndTime,
                    Recipient= c.Recipient,
                    Reference = c.Reference
                }).ToListAsync();
        }
    }
}
