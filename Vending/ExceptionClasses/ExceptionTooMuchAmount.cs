using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductVendingMachine
{
    public class ExceptionTooMuchAmount : System.Exception
    {
        public ExceptionTooMuchAmount(string message) : base(message)
        {
        }
    }
}
