using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model;
using AppActs.Client.Repository.Interface;
using MongoDB.Bson;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.Repository.Interface
{
    public interface ITileRepository
    {

        Tile Get(TileType tileType, string query, Guid applicationId, DateTime dateStart, DateTime dateEnd);

        Tile Get(TileType tileType, string query, Guid applicationId, DateTime dateStart, DateTime dateEnd, 
            DateTime dateStartCompare, DateTime dateEndCompare);
    }
}
