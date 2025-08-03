using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Lib.Model
{
    public class Operation
    {
        public bool  Success { get; set; }
        public object OperationId { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
