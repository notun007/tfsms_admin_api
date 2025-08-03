using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Technofair.SMS.Filters
{
    public class ButtonPermission
    {
        public static bool Add { get; set; }
        public static bool ReadOnly { get; set; }
        public static bool Edit { get; set; }
        public static bool Delete { get; set; }
        public static bool Print { get; set; }
    }
}