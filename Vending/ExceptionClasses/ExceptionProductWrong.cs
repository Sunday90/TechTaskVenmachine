using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductVendingMachine
{
    public class ExceptionIncorrectProduct : System.Exception
    {
        public ExceptionIncorrectProduct(string message) : base (message)
        {
        }
    }
}
