using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UnitOfWork
{
    public interface IAppUnitOfWork
    {
        IProductService Product { get; } 
    }
}
