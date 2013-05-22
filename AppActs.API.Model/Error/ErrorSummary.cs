using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Error
{
    public class ErrorSummary : Summary
    {
        public List<Aggregate<string>> ScreenErrors { get; set; }

        public ErrorSummary()
        {

        }

        public ErrorSummary(Error error)
            : base(error)
        {
            this.ScreenErrors = new List<Aggregate<string>>();
            this.ScreenErrors.Add(new Aggregate<string>(error.ScreenName));
        }
    }
}
