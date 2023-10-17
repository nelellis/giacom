using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.ViewModels.Requests
{

    public class FilterCriteriaBase: IDateFilter
    {
        public string? CallerId { get; set; }
        public string? Recipient { get; set; }
        public DateTime? CallDateFrom { get; set; }
        public DateTime? CallDateTo { get; set; }
    }

    public interface IDateFilter
    {
        DateTime? CallDateFrom { get; set; }
        DateTime? CallDateTo { get; set; }
    }
}
