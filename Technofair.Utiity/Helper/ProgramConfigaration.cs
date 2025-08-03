using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Helper
{
    public static class ProgramConfigaration
    {
        public static string WebRootPath { get; set; }
        public static string ConnectionString { get; set; }
        public static bool IsProduction { get; set; }
    }
}

