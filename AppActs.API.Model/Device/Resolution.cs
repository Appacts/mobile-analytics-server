using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace AppActs.API.Model.Device
{
    public class Resolution
    {
        [BsonElement("W")]
        public int Width { get; set; }

        [BsonElement("H")]
        public int Height { get; set; }

        [BsonElement("WxH")]
        public string WidthxHeight { get; set; }

        public int Count { get; set; }

        public Resolution(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.WidthxHeight = string.Concat(width, "x", height);
        }

        public Resolution CopyOnlyKeys()
        {
            return new Resolution(this.Width, this.Height);
        }
    }
}
