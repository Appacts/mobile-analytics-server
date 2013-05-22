using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Device
{
    public class ManufacturerModelAggregate : Aggregate<string>
    {
        public string Model { get; set; }
        public string ManufacturerModel { get; set; }

        public ManufacturerModelAggregate(string manufacturer, string model)
            : base(manufacturer)
        {
            this.Model = model;
            this.ManufacturerModel = string.Concat(manufacturer, model);
        }

        public new ManufacturerModelAggregate CopyOnlyKeys()
        {
            return new ManufacturerModelAggregate(this.Key, this.Model);
        }
    }
}
