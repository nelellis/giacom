using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler.ViewModels.Requests;

namespace DataHandler.ViewModels.Stats.Requests
{
    public class LongestCallsRequest : FilterCriteriaBase
    {
        [Required, Range(1, 25)]
        public int NumberOfLongestCalls { get; set; }
    }
}
