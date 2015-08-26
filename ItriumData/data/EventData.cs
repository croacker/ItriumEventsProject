using System;

namespace ItriumData.data
{
    /// <summary>
    /// Новый класс для события
    /// </summary>
    class EventData
    {
        public int ID { get; set; }
        public DateTime dateTime { get; set; }
        public virtual CredentialHolder credentialHolder { get; set; }
        public string сard { get; set; }
        public string headline { get; set; }
        public string clockNumber { get; set; }
        public virtual EventOriginalData originalData { get; set; }
        public virtual EventSource eventSource { get; set; }
    }
}
