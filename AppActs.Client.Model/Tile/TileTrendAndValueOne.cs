using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace AppActs.Client.Model
{
    public class TileTrendAndValueOne : TileWithTrend
    {
        public object ValueOne { get; set; }

        public override bool IsEmpty()
        {
            return this.ValueOne == null;
        }
    }
}
