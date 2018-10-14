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
        private string eventTitle;
        private int eventID;
        private string eventDescription;
        private DateTimeOffset date;
        private bool registration;
        private string eventDetail;
        private BasicGeoposition defaultposition;
        private string myphotoname;


        public Event(string eventTitle, int eventID, string eventDescription, DateTimeOffset date, bool registration, string eventDetail, BasicGeoposition defaultposition, string myphotoname)
        {
            this.eventTitle = eventTitle;
            this.eventID = eventID;
            this.eventDescription = eventDescription;
            this.date = date;
            this.registration = registration;
            this.eventDetail = eventDetail;
            this.defaultposition = defaultposition;
            this.myphotoname = myphotoname;
        }
    }
}
