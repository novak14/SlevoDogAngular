using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface BusinessObject<T>
    {
        T Select();
    }
}
