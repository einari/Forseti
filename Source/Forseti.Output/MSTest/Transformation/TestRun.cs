using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.Output.MSTest.Transformation
{
    public class TestRun : ITransformToTrx
    {
        const string _elementName = "TestRun";

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RunUser { get; set; }
        public string   ComputerName  { get; set; }
        public DateTime CreatedAt { get; set; }

        public string FormatedName { get { return GetFormatedRunName(Name ?? "default", ComputerName ?? "local", CreatedAt ); } }

        public TestRun() 
        {
            CreatedAt = DateTime.Now;
        }

        public XElement TransformToTrx()
        {
            var element = new XElement(TrxBuilder.XMLNS + _elementName);
            element.SetAttributeValue("id", Id);
            element.SetAttributeValue("name", FormatedName);
            element.SetAttributeValue("runUser", RunUser);

            return element;
        }

        string GetFormatedRunName(string runUser, string computerName, DateTime runCreatedTime)
        {
            var name = string.Format("{0}@{1} {2}", GetUserWithoutDomain(runUser), computerName, runCreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
           return name;
        }

        string GetUserWithoutDomain(string userName) 
        {
            var userNameParts = userName.Split(new []{"\\"},StringSplitOptions.RemoveEmptyEntries);
            if (userNameParts.Length > 2)
                return userNameParts[1];

            return userName;
        }
    }
}
