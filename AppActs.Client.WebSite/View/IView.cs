using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Mvp.View;

namespace AppActs.Client.View
{
    public interface IView : IViewBase
    {
        bool IsSecureConnection { get; }
        void RedirectToSecure();
    }
}
