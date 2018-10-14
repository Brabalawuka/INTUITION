using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;


namespace INTUITION
{
    public class Event
    {
        private string Title { get; set; }
        private int Id { get; set; }
        private string OneSentenceDiscription { get; set; }
        private string Venue { get; set; }
        private DateTime Datetime { get; set; }
        private bool Isregistrationrequired { get; set; }
        private string Detail { get; set; }
        private BasicGeoposition Position { get; set; }
        private string Imagelocation { get; set; }

        public Event(string title, int id, string Osd, string venue, DateTime datetime, bool Irr, string detail, BasicGeoposition eventposition, string imagelocation)
        {
            Title = title;
            Id = id;
            OneSentenceDiscription = OneSentenceDiscription;
            Venue = venue;
            Isregistrationrequired = Irr;
            Detail = detail;
            Position = eventposition;
            Imagelocation = imagelocation;
        }

    }
}
