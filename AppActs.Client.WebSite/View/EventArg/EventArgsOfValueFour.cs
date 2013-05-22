using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.View.EventArg
{
    public class EventArgs<TValueOne, TValueTwo, TValueThree, TValueFour> 
        : EventArgs<TValueOne, TValueTwo, TValueThree>
    {
        /// <summary>
        /// Gets the value four.
        /// </summary>
        public TValueFour ValueFour { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs&lt;TValueOne, TValueTwo, TValueThree, TValueFour&gt;"/> class.
        /// </summary>
        /// <param name="valueOne">The value one.</param>
        /// <param name="valueTwo">The value two.</param>
        /// <param name="valueThree">The value three.</param>
        /// <param name="valueFour">The value four.</param>
        public EventArgs(TValueOne valueOne, TValueTwo valueTwo, 
            TValueThree valueThree, TValueFour valueFour)
            : base(valueOne, valueTwo, valueThree)
        {
            this.ValueFour = valueFour;
        }
    }
}
