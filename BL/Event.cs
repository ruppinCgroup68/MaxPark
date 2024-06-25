using MaxPark.DAL;
using Microsoft.AspNetCore.Identity;
using projMaxPark.DAL;
using System.ComponentModel;

namespace MaxPark.BL
{
    public class Event
    {
        int eventId;
        int userId;
        string userCarNum;
        string eventType;

        public Event()
        {
        }

        public Event(int eventId, int userId, string userCarNum, string eventType)
        {
            EventId = eventId;
            UserId = userId;
            UserCarNum = userCarNum;
            EventType = eventType;
        }

        public int EventId { get => eventId; set => eventId = value; }
        public int UserId { get => userId; set => userId = value; }
        public string UserCarNum { get => userCarNum; set => userCarNum = value; }
        public string EventType { get => eventType; set => eventType = value; }

        public List<Event> readEvents()
        {
            DBServicesEvents dbs = new DBServicesEvents();
            return dbs.getEvents();
        }

        public int addNewEvent(Event ev)
        {
            DBServicesEvents dbs = new DBServicesEvents();
            return dbs.addNewEvent(ev);
        }


        public int deleteEvent(int id)
        {
            DBServicesEvents dbs = new DBServicesEvents();
            return dbs.deleteEvent(id);
        }

    }

} 

