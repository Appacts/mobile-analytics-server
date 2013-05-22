using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class SessionLengthGroup
    {
        public int _10sec { get; set; }
        public int _20sec { get; set; }
        public int _30sec { get; set; }
        public int _1min { get; set; }
        public int _2min { get; set; }
        public int _4min { get; set; }
        public int _8min { get; set; }
        public int _16min { get; set; }
        public int _32min { get; set; }
        public int _1hr { get; set; }
        public int Over1hr { get; set; }

        public SessionLengthGroup()
        {

        }

        public SessionLengthGroup(long timeInSeconds)
        {
            if (timeInSeconds <= 10)
            {
                this._10sec += 1;
            }
            else if (timeInSeconds > 10 && timeInSeconds <= 20)
            {
                this._20sec += 1;
            }
            else if (timeInSeconds > 20 && timeInSeconds <= 30)
            {
                this._30sec += 1;
            }
            else if (timeInSeconds > 30 && timeInSeconds <= 60)
            {
                this._1min += 1;
            }
            else if (timeInSeconds > 60 && timeInSeconds <= 120)
            {
                this._2min += 1;
            }
            else if (timeInSeconds > 120 && timeInSeconds <= 240)
            {
                this._4min += 1;
            }
            else if (timeInSeconds > 240 && timeInSeconds <= 480)
            {
                this._8min += 1;
            }
            else if (timeInSeconds > 480 && timeInSeconds <= 960)
            {
                this._16min += 1;
            }
            else if (timeInSeconds > 960 && timeInSeconds <= 1920)
            {
                this._32min += 1;
            }
            else if (timeInSeconds > 1920 && timeInSeconds <= 3600)
            {
                this._1hr += 1;
            }
            else
            {
                this.Over1hr += 1;
            }
        }
    }
}
