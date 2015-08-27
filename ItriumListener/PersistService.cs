using System;
using System.Collections.Generic;
using System.Linq;
using ItriumCls;
using ItriumData.data;

namespace ItriumListener
{
    public class PersistService: IPersistService
    {
        public void persistEvent(Dictionary<string, string> data, EventOriginalData eventOriginalData)
        {
            if (data.Keys.Count != 0)
            {
                using (ItriumDbContext db = new ItriumDbContext())
                {
                    CredentialHolder holder = getCredentialHolderByName(data["Data.CredentialHolderName"].Trim(), db);

                    EventSource eventSource = getEventSourceByAPName(data["Source.AccessPointName"], db);
                    eventSource.accessPointToken = data["Source.AccessPointToken"];//Возможно нужно искать по этому параметру
                    eventSource.nameSomeData = data["Source.NameSomeData"];

                    EventData eventData = new EventData
                    {
                        dateTime = DateTime.Now,
                        credentialHolder = holder,
                        сard = data["Data.Card"],
                        headline = data["Data.Headline"],
                        clockNumber = data["Data.ClockNumber"],
                        credentialToken = data["Data.CredentialToken"],
                        originalData = eventOriginalData,
                        eventSource = eventSource
                    };

                    db.EventData.Add(eventData);
                    db.SaveChanges();
                }
            }
        }

        public void persistError(Exception exception)
        {
            using (var db = new ItriumDbContext())
            {
                var errorData = new ErrorData
                {
                    errorDate = DateTime.Now,
                    title = exception.Message,
                    msg = exception.Message
                };
                db.ErrorData.Add(errorData);
                db.SaveChanges();
            }
        }

        public void persistError(string title, Exception exception)
        {
            using (var db = new ItriumDbContext())
            {
                var errorData = new ErrorData
                {
                    errorDate = DateTime.Now,
                    title = title,
                    msg = exception.Message
                };
                db.ErrorData.Add(errorData);
                db.SaveChanges();
            }
        }

        public CredentialHolder getCredentialHolderByName(string name, ItriumDbContext db)
        {
            CredentialHolder holder = null;
            var holders = db.CredentialHolder.Where(ch => ch.name.Equals(name));
            holder = !holders.Any() ? addNewCredentialHolder(name, db) : holders.First();
            return holder;
        }

        private CredentialHolder addNewCredentialHolder(string name, ItriumDbContext db)
        {
            var holder = new CredentialHolder {name = name};
            db.CredentialHolder.Add(holder);
            return holder;
        }

        public EventSource getEventSourceByAPName(string accessPointName, ItriumDbContext db)
        {
            EventSource eventSource = null;
            var eventSources = db.EventSource.Where(ch => ch.accessPointName.Equals(accessPointName));
            eventSource = !eventSources.Any() ? addNewEventSource(accessPointName, db) : eventSources.First();
            return eventSource;
        }

        private EventSource addNewEventSource(string accessPointName, ItriumDbContext db)
        {
            return new EventSource { accessPointName = accessPointName };
        }

        internal EventOriginalData persistEventOriginal(string requestData)
        {
            EventOriginalData eventOriginalData;
            using (var db = new ItriumDbContext())
            {
                eventOriginalData = new EventOriginalData
                {
                    originalData = requestData,
                    dateTime = DateTime.Now
                };
                db.EventOriginalData.Add(eventOriginalData);
                db.SaveChanges();
            }
            return eventOriginalData;
        }
    }
}