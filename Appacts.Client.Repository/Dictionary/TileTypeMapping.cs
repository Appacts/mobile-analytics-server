using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model.Enum;
using AppActs.Client.Model;

namespace AppActs.Client.Repository.Dictionary
{
    public class TileTypeMapping
    {
        private static Dictionary<TileType, Type> tileTypeToType = new Dictionary<TileType, Type>()
        {
            { TileType.ValueOne, typeof(TileWithValueOne) },
            { TileType.ValueTwo, typeof(TileWithValueTwo) },
            { TileType.ValueThree, typeof(TileWithValueThree) },
            { TileType.TrendAndValueOne, typeof(TileTrendAndValueOne) },
            { TileType.TrendAndValueTwo, typeof(TileTrendAndValueTwo) },
            { TileType.TrendAndValueThree, typeof(TileTrendAndValueThree) },
        };

        public static Type Get(TileType tileType)
        {
            return tileTypeToType[tileType];
        }
    }
}
