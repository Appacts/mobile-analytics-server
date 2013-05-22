using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.User
{
    public class AgeGroup
    {
        public int _1_18 { get; set; }
        public int _19_24 { get; set; }
        public int _25_35 { get; set; }
        public int _36_50 { get; set; }
        public int _51_69 { get; set; }
        public int _71 { get; set; }

        public AgeGroup(Nullable<int> age)
        {
            if (!age.HasValue)
                return;

            if (age <= 18)
            {
                this._1_18 += 1;
            }
            else if (age > 18 && age <= 24)
            {
                this._19_24 += 1;
            }
            else if (age > 24 && age <= 35)
            {
                this._25_35 += 1;
            }
            else if (age > 35 && age <= 50)
            {
                this._36_50 += 1;
            }
            else if (age > 50 && age <= 69)
            {
                this._51_69 += 1;
            }
            else
            {
                this._71 += 1;
            }
        }

    }
}
