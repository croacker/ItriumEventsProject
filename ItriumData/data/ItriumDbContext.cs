using System.Data.Entity;

namespace ItriumData.data
{
    public class ItriumDbContext : DbContext
    {
        public ItriumDbContext() : base("ItriumEventsDb") { }
        public DbSet<CredentialHolder> CredentialHolders { get; set; }
        public DbSet<ItriumEventData> ItriumEventsDatas { get; set; }
        public DbSet<ErrorData> ErrorDatas { get; set; }
        public DbSet<PropertiesData> PropertiesDatas { get; set; }
        public DbSet<EventOriginalData> EventOriginalDatas { get; set; }
    }
}
