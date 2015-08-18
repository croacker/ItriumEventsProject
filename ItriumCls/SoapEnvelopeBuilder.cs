using System.Xml;

namespace ItriumCls
{
    static class SoapEnvelopeBuilder
    {
        public static XmlDocument newSubscribe()
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" 
                        xmlns:wsa=""http://www.w3.org/2005/08/addressing"" 
                        xmlns:wsnt=""http://docs.oasis-open.org/wsn/b-2"" xmlns:onvif=""http://www.onvif.org/ver10/schema"" xmlns:ns1=""urn:ru:itrium:neyross:1.0"">
                    <soap:Body>
                        <wsnt:Subscribe>
 		                    <wsnt:ConsumerReference>
 			                    <wsa:Address>" + AppProperties.EventListenerAddress
                                               + "</wsa:Address> </wsnt:ConsumerReference> </wsnt:Subscribe> </soap:Body></soap:Envelope>");
            return soapEnvelop;
        }

        public static XmlDocument newRenew(string terminationTime)
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" 
                        xmlns:wsa=""http://www.w3.org/2005/08/addressing"" 
                        xmlns:wsnt=""http://docs.oasis-open.org/wsn/b-2"" xmlns:onvif=""http://www.onvif.org/ver10/schema"" xmlns:ns1=""urn:ru:itrium:neyross:1.0"">
                    <soap:Body>
                        <wsnt:Renew>
      <wsnt:TerminationTime>" + terminationTime + "</wsnt:TerminationTime>"
     + "</wsnt:Renew></soap:Body></soap:Envelope>"
                    );
            return soapEnvelop;
        }

        public static XmlDocument newUnsubscribe()
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" 
                        xmlns:wsa=""http://www.w3.org/2005/08/addressing"" 
                        xmlns:wsnt=""http://docs.oasis-open.org/wsn/b-2"" xmlns:onvif=""http://www.onvif.org/ver10/schema"" xmlns:ns1=""urn:ru:itrium:neyross:1.0"">
                    <soap:Body>
                         <wsnt:Unsubscribe />
                    </soap:Body>
                </soap:Envelope>"
                    );
            return soapEnvelop;
        }
    }
}
