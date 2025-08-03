using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Key
{
    public class KeyGeneration
    {
        public static string GenerateTimestamp()
        {
            string timestamp = "TF" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return timestamp;
        }
    }
}
