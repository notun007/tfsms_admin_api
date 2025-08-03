using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Conversion
{
    public static class DateCoverter
    {
        public static string DateToStringAsddMMyyyy(DateTime? date)
        {
            string formattedDate = string.Empty;
            if (date == null)
            {
                return string.Empty;
            }
            try
            {
                formattedDate = date.Value.ToString("dd/MM/yyyy");
            }
            catch(Exception ex)
            {
                formattedDate = string.Empty;
            }
            return formattedDate;
        }
    }
}
