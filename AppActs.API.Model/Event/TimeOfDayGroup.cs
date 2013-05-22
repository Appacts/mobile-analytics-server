using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class TimeOfDayGroup
    {
        public int _0 { get; set; }
        public int _1 { get; set; }
        public int _2 { get; set; }
        public int _3 { get; set; }
        public int _4 { get; set; }
        public int _5 { get; set; }
        public int _6 { get; set; }
        public int _7 { get; set; }
        public int _8 { get; set; }
        public int _9 { get; set; }
        public int _10 { get; set; }
        public int _11 { get; set; }
        public int _12 { get; set; }
        public int _13 { get; set; }
        public int _14 { get; set; }
        public int _15 { get; set; }
        public int _16 { get; set; }
        public int _17 { get; set; }
        public int _18 { get; set; }
        public int _19 { get; set; }
        public int _20 { get; set; }
        public int _21 { get; set; }
        public int _22 { get; set; }
        public int _23 { get; set; }

        public Tuple<string, int> PropertyAndValue { get; set; }

        public TimeOfDayGroup()
        {

        }

        public TimeOfDayGroup(int timeOfDay)
        {
            switch (timeOfDay)
            {
                case 0: 
                    this._0 += 1;
                    break;
                case 1:
                    this._1 += 1;
                    break;
                case 2:
                    this._2 += 1;
                    break;
                case 3:
                    this._3 += 1;
                    break;
                case 4:
                    this._4 += 1;
                    break;
                case 5:
                    this._5 += 1;
                    break;
                case 6:
                    this._6 += 1;
                    break;
                case 7:
                    this._7 += 1;
                    break;
                case 8:
                    this._8 += 1;
                    break;
                case 9:
                    this._9 += 1;
                    break;
                case 10:
                    this._10 += 1;
                    break;
                case 11:
                    this._11 += 1;
                    break;
                case 12:
                    this._12 += 1;
                    break;
                case 13:
                    this._13 += 1;
                    break;
                case 14:
                    this._14 += 1;
                    break;
                case 15:
                    this._15 += 1;
                    break;
                case 16:
                    this._16 += 1;
                    break;
                case 17:
                    this._17 += 1;
                    break;
                case 18:
                    this._18 += 1;
                    break;
                case 19:
                    this._19 += 1;
                    break;
                case 20: 
                    this._20 += 1;
                    break;
                case 21:
                    this._21 += 1;
                    break;
                case 22:
                    this._22 += 1;
                    break;
                case 23:
                    this._23 += 1;
                    break;
            }

            this.setPropertyAndValue(String.Concat("_", timeOfDay), 1);
        }

        private void setPropertyAndValue(string name, int value)
        {
            this.PropertyAndValue = new Tuple<string, int>(name, value);
        }
    }
}
