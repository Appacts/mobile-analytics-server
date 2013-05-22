using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Specialized;

namespace AppActs.API.Model
{
    public class ItemWithScreen : Item
    {
        [BsonElement("scrn")]
        public string ScreenName { get; set; }

        public ItemWithScreen()
            : base()
        {

        }

        public ItemWithScreen(NameValueCollection keyValues)
            : base(keyValues)
        {
            this.ScreenName = keyValues[Keys.SCREEN_NAME];
        }
    }
}
