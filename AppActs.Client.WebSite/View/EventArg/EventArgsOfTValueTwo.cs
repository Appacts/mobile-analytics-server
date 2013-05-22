using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.View.EventArg
{
    public class EventArgs<TValueOne, TValueTwo> : EventArgs<TValueOne>
    {
        /// <summary>
        /// Gets the value two.
        /// </summary>
        public TValueTwo ValueTwo { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs&lt;TValueOne, TValueTwo&gt;"/> class.
        /// </summary>
        /// <param name="valueOne">The value one.</param>
        /// <param name="valueTwo">The value two.</param>
        public EventArgs(TValueOne valueOne, TValueTwo valueTwo)
            : base(valueOne)
        {
            this.ValueTwo = valueTwo;
        }
    }
}
