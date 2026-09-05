using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.Common
{
    public class PingResponseViewModel
    {
        public string DeviceName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }

        public string Status { get; set; }
        public long RoundtripTime { get; set; }
        public string Message { get; set; }
    }
}
