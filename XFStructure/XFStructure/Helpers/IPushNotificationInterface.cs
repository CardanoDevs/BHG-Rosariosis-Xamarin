using System;
using System.Collections.Generic;
using System.Text;

namespace XFStructure.Helpers
{
    public interface IPushNotificationInterface
    {
        string GetToken();
        void LogDebug(string data);
    }

    public class FireBasePushNotificationResponseEventsArgs:EventArgs
    {
        public string Identifier { get; set; }
        public Dictionary<string,object> Data { get; set; }
        public string Type { get; set; }
    }
}
