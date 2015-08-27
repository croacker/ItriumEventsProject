using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Новый класс для источника события
/// </summary>
namespace ItriumData.data
{
    public class EventSource
    {
        public int ID { get; set; }
        public string accessPointToken { get; set; }
        public string accessPointName { get; set; }
        public string nameSomeData { get; set; }
    }
}
