﻿using System.Data.Entity;

namespace ItriumData.data
{
    public class ItriumDbContext : DbContext
    {
        public ItriumDbContext() : base("ItriumEventsDb") { }
        public DbSet<CredentialHolder> CredentialHolder { get; set; }
        public DbSet<ErrorData> ErrorData { get; set; }
        public DbSet<AppProperty> AppProperty { get; set; }
        public DbSet<EventOriginalData> EventOriginalData { get; set; }
        public DbSet<EventData> EventData { get; set; }
        public DbSet<EventSource> EventSource { get; set; }
    }
}
