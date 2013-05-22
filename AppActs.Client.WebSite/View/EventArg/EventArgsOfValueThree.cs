using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.View.EventArg
{
    public class EventArgs<TValueOne, TValueTwo, TValueThree> 
        : EventArgs<TValueOne, TValueTwo>
    {
        /// <summary>
        /// Gets the value three.
        /// </summary>
        public TValueThree ValueThree { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs&lt;TValueOne, TValueTwo, TValueThree&gt;"/> class.
        /// </summary>
        /// <param name="valueOne">The value one.</param>
        /// <param name="valueTwo">The value two.</param>
        /// <param name="valueThree">The value three.</param>
        public EventArgs(TValueOne valueOne, TValueTwo valueTwo, TValueThree valueThree)
            : base(valueOne, valueTwo)
        {
            this.ValueThree = valueThree;
        }
    }
}
