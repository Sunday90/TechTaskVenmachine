using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductVendingMachine
{
    class ExceptionAlreadyBought : Exception
    {
        public ExceptionAlreadyBought(string message) : base(message)
        {
        }
    }
}
