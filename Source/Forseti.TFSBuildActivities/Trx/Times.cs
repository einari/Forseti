using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TFSBuildActivities.Trx
{
    public class Times : IConvertToTrx
    {
        const string _elementName = "Times";

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public XElement ConvertToTrxNode()
        {
            var element = new XElement(_elementName);
            element.SetAttributeValue("start", StartTime);
            element.SetAttributeValue("end", EndTime);
            return element;
        }
    }
}
