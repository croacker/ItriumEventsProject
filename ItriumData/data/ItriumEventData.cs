namespace ItriumData.data
{
    public class ItriumEventData
    {
        public int ID { get; set; }
        public CredentialHolder credentialHolder { get; set; }
        public string ClockNumber { get; set; }
        public string Name { get; set; }
        public EventOriginalData OriginalData { get; set; }
    }
}
