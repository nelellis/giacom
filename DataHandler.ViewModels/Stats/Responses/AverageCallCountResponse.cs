using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.ViewModels.Stats.Responses
{
    public class AverageCallCountResponse
    {
        public double AverageCallCountPerDay { get; set; }

        public double TotalDays { get; set; }
    }
}
