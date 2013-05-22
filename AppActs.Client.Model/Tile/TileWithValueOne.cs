using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model;
using MongoDB.Bson;

namespace AppActs.Client.Model
{
    public class TileWithValueOne : Tile
    {
        public object ValueOne { get; set; }

        public override bool IsEmpty()
        {
            return this.ValueOne == null;
        }
    }
}
