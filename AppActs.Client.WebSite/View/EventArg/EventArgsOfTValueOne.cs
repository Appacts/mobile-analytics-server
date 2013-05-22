using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.View.EventArg
{
    public class EventArgs<TValueOne> : EventArgs
    {
        /// <summary>
        /// Gets the value one.
        /// </summary>
        public TValueOne ValueOne { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs&lt;TValueOne&gt;"/> class.
        /// </summary>
        /// <param name="valueOne">The value one.</param>
        public EventArgs(TValueOne valueOne)
        {
            this.ValueOne = valueOne;
        }
    }
}
