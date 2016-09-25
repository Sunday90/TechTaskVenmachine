using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductVendingMachine
{
    public class ExceptionIncorrectCoin : Exception
    {
        public ExceptionIncorrectCoin(string message) : base(message)
        {
        }
    }
}
