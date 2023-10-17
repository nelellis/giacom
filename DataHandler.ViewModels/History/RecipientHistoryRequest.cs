using DataHandler.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.ViewModels.History
{
    public class RecipientHistoryRequest : IDateFilter, IPaginatedRequest
    {
        public DateTime? CallDateFrom { get; set; }
        public DateTime? CallDateTo { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
