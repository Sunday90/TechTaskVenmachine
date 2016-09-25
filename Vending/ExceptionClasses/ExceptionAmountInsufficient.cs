using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductVendingMachine
{
    public class ExceptionNotEnoughAmount : Exception
    {
        public ExceptionNotEnoughAmount(string message) : base(message)
        {
        }
    }
}
