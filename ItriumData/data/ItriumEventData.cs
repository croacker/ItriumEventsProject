namespace ItriumData.data
{
    public class ItriumEventData
    {
        public int ID { get; set; }
        public string typeName { get; set; }
        public CredentialHolder credentialHolder { get; set; }
        public string clockNumber { get; set; }
        public EventOriginalData originalData { get; set; }
    }
}
