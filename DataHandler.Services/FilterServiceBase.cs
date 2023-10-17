using DataHandler.Entities;
using DataHandler.Repositories;
using DataHandler.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataHandler.Services
{
    public abstract class FilterServiceBase
    {
        private readonly ICallDetailRecordRepository _callDetailRecordRepo;
        protected FilterServiceBase(ICallDetailRecordRepository callDetailRecordRepo) { 
            _callDetailRecordRepo= callDetailRecordRepo;
        }
        public IQueryable<CallDetailRecord> GetQueryable(FilterCriteriaBase request)
        {
            var query = _callDetailRecordRepo.GetQueriable();
            if (!string.IsNullOrWhiteSpace(request.CallerId))
            {
                query = query.Where(c => c.CallerId == request.CallerId);
            }
            if (!string.IsNullOrWhiteSpace(request.Recipient))
            {
                query = query.Where(c => c.Recipient == request.Recipient);
            }
            return ApplyDateFilter(query, request);
        }

        public IQueryable<CallDetailRecord> ApplyDateFilter(IQueryable<CallDetailRecord> query, IDateFilter request)
        {
            
            if (request.CallDateFrom.HasValue)
            {
                query = query.Where(c => c.CallDate >= request.CallDateFrom.Value);
            }
            if (request.CallDateTo.HasValue)
            {
                query = query.Where(c => c.CallDate <= request.CallDateTo.Value);
            }
            return query;
        }
        public IQueryable<CallDetailRecord> PaginatedQuery(IQueryable<CallDetailRecord> query, IPaginatedRequest request)
        {
            var pageIndex = request.PageIndex ?? 1;
            var pageSize = request.PageSize ?? 20;
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
