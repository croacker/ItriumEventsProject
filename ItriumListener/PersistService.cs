using System.Collections.Generic;
using System.Linq;
using ItriumData.data;

namespace ItriumListener
{
    public class PersistService
    {
        public void persistEvent(Dictionary<string, string> data)
        {
            using (ItriumDbContext db = new ItriumDbContext())
            {
                var holder = getCredentialHolderByName("name", db);
            }
        }

        public void persistError(string msg)
        {

        }

        public CredentialHolder getCredentialHolderByName(string name, ItriumDbContext db)
        {
            CredentialHolder holder = null;
            var holders = db.CredentialHolders.Where(ch => ch.Name.Equals(name)).ToList();
            if (holders.Count == 0)
            {
                holder = addNewCredentialHolder(name, db);
            }
            else
            {
                holder = holders[0];
            }
            return holder;
        }

        private CredentialHolder addNewCredentialHolder(string name, ItriumDbContext db)
        {
            var holder = new CredentialHolder {Name = name};
            db.CredentialHolders.Add(holder);
            return holder;
        }
    }
}