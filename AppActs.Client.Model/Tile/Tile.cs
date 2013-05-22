using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model.Enum;
using AppActs.Client.Model;
using MongoDB.Bson;

namespace AppActs.Client.Model
{
    public abstract class Tile
    {
        public Tile()
        {

        }

        public abstract bool IsEmpty();
    }
}
