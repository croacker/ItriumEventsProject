using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;

namespace ItriumCls
{
    public class WriteDataService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WriteDataService));

        public void writeData(Dictionary<string, string> data)
        {
            log.Info("Write event data: " + data);
            using (var streamWriter = new StreamWriter(AppProperties.RESULT_FILE_NAME, true, Encoding.Default))
            {
                string line = DateTime.Now + " data: ";
                foreach (var key in data.Keys)
                {
                    line += "[" + key + "]:" + data[key] + ", ";
                }
                streamWriter.WriteLine(line);
            }
        }
    }
}
