using System;
using System.Collections.Generic;
using System.Linq;
using ItriumData.data;

namespace ItriumListener
{
    public class PersistService
    {
        public void persistEvent(Dictionary<string, string> data)
        {
            if (data.Keys.Count != 0)
            {
                CredentialHolder holder;
                using (ItriumDbContext db = new ItriumDbContext())
                {
                    holder = getCredentialHolderByName(data["credentialHolderName"], db);
                    ItriumEventData itriumEventData = new ItriumEventData
                    {
                        credentialHolder = holder,
                        ClockNumber = data["clockNumber"],
                        Name = "event"
                    };
                    db.ItriumEventsDatas.Add(itriumEventData);
                    db.SaveChanges();
                }
            }
        }

        public void persistError(string msg)
        {
        }

        public CredentialHolder getCredentialHolderByName(string name, ItriumDbContext db)
        {
            CredentialHolder holder = null;
            var holders = db.CredentialHolders.Where(ch => ch.Name.Equals(name));
            holder = !holders.Any() ? addNewCredentialHolder(name, db) : holders.First();
            return holder;
        }

        private CredentialHolder addNewCredentialHolder(string name, ItriumDbContext db)
        {
            var holder = new CredentialHolder {Name = name};
            db.CredentialHolders.Add(holder);
            return holder;
        }

        internal void persistEventOriginal(string requestData)
        {
            using (ItriumDbContext db = new ItriumDbContext())
            {
                EventOriginalData eventOriginalData = new EventOriginalData();
                eventOriginalData.OriginalData = requestData;
                db.EventOriginalDatas.Add(eventOriginalData);
                db.SaveChanges();
            }
        }
    }
}