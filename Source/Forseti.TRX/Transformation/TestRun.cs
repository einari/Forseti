using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TRX.Transformation
{
    public class TestRun : ITransformToTrx
    {
        const string _elementName = "TestRun";

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RunUser { get; set; }


        public XElement TransformToTrx()
        {
            var element = new XElement(TrxBuilder.XMLNS + _elementName);
            element.SetAttributeValue("id", Id);
            element.SetAttributeValue("name", Name);
            element.SetAttributeValue("runUser", RunUser);

            return element;
        }
    }
}
