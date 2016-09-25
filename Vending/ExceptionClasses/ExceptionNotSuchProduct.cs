using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductVendingMachine
{
    public class ExceptionNotSuchProduct : System.Exception
    {
        public ExceptionNotSuchProduct(string message) : base (message)
        {
        }
    }
}
