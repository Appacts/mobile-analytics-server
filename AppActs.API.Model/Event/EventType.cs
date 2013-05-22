using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Enum
{
    public enum EventType
    {
        ApplicationOpen = 1,
        ApplicationClose = 2,
        Error = 3,
        Event = 4,
        Feedback = 5,
        ScreenClose = 6,
        ContentLoaded = 7,
        ContentLoading = 8,
        ScreenOpen = 9
    }
}
