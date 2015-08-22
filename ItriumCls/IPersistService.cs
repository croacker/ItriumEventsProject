using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItriumCls
{
    public interface IPersistService
    {
        void persistError(Exception exception);
        void persistError(string title, Exception exception);
    }
}
