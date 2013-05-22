using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.View
{
    public interface IErrorView : IView
    {
        Exception GetException();
    }
}
