using CsvHelper.Configuration;
using DataHandler.Entities;
using System.Globalization;

namespace DataHandler.CsvMapper
{
    public class CsvCallDetailRecordMap: ClassMap<CallDetailRecord>
    {
        public CsvCallDetailRecordMap()
        {
            string dateFormat = "dd/MM/yyyy";
            var msRE = CultureInfo.GetCultureInfo("ms-RE");

            Map(m => m.Reference).Name("reference");
            Map(m => m.CallerId).Name("caller_id");
            Map(m => m.Recipient).Name("recipient");
            Map(m => m.CallDate).Name("call_date").TypeConverterOption.Format(dateFormat)
          .TypeConverterOption.CultureInfo(msRE);
            Map(m => m.EndTime).Name("end_time");
            Map(m => m.Duration).Name("duration");
            Map(m => m.Cost).Name("cost");
            Map(m => m.Currency).Name("currency");
        }
    }
}