using Common.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure
{
    public interface IStateObject
    {
        ObjectState State { get; }
    }
}
