using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DataStore.Customization.Responses.ClassRoom
{
    public class Annoucement
    {
        public List<AnnouncementDetail> Announcements { get; set; }
        
        public class AnnouncementDetail
        {
            public string Title { get; set; }
            public string Text { get; set; }
            public string Type { get; set; }
            public Color BgColor { get; set; }
        }
    }
}
