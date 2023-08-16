using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Exceptions
{
    public class PermanentException : Exception
    {
        public PermanentException(int requestId, string message = "", Exception innerException = null) : base(message: message, innerException: innerException)
        {
            RequestId = requestId;
        }
        public int RequestId { get; set; }
    }
}
