using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class ScreenRouteSummary : Summary
    {
        public List<ScreenRoute> ScreenRoutes { get; set; }

        public ScreenRouteSummary()
        {

        }

        public ScreenRouteSummary(Event evenItem, string lastScreen, string currentScreen)
            : base(evenItem)
        {
            this.ScreenRoutes = new List<ScreenRoute>();
            this.ScreenRoutes.Add(new ScreenRoute(lastScreen, currentScreen));
        }
    }
}
