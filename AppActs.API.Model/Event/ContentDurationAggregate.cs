using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class ContentDurationAggregate : DurationAggregate
    {
        public string Content { get; set; }
        public string ScreenContent { get; set; }

        public ContentDurationAggregate(string screen, string content, long seconds)
            : base(screen, seconds)
        {
            this.Content = content;
            this.ScreenContent = String.Concat(screen, content);
        }

        public ContentDurationAggregate CopyOnlyKey()
        {
            return new ContentDurationAggregate(this.Key, this.Content, 0);
        }
    }
}
