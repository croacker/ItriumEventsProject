using System;

namespace ItriumData.data
{
    public class ItriumEventData
    {
        public int ID { get; set; }
        public DateTime dateTime { get; set; }
        public string typeName { get; set; }
        public virtual CredentialHolder credentialHolder { get; set; }
        public string clockNumber { get; set; }
        public virtual EventOriginalData originalData { get; set; }
    }
}
