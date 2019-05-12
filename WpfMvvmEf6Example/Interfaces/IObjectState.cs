using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvmEf6Example.Interfaces
{
    public interface IObjectState
    {
        ItemState State { get; set; }
    }
}
