using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.ViewModels.Requests
{
    public interface IPaginatedRequest
    {
        int? PageIndex { get; set; }
        int? PageSize { get; set; }
    }
    public class PaginatedRequest : IPaginatedRequest
    {
        public int? PageIndex { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}
