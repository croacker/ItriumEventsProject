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
                CredentialHolder holder;
                using (ItriumDbContext db = new ItriumDbContext())
                {
                    holder = getCredentialHolderByName(data["credentialHolderName"], db);
                    ItriumEventData itriumEventData = new ItriumEventData
                    {
                        dateTime = DateTime.Now,
                        credentialHolder = holder,
                        clockNumber = data["clockNumber"],
                        typeName = "event",
                        originalData = eventOriginalData
                    };
                    db.ItriumEventsData.Add(itriumEventData);
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