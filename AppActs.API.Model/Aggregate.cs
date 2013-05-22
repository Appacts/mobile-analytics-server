using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model
{
    public class Aggregate<TKey>
    {
        public TKey Key { get; set; }
        public int Count { get; set; }

        public Aggregate()
        {

        }

        public Aggregate(TKey key)
        {
            this.Key = key;
        }

        public Aggregate<TKey> CopyOnlyKeys()
        {
            return new Aggregate<TKey>(this.Key);
        }
    }
}
