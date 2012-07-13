using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Growl.Connector;
using Growl.CoreLibrary;

namespace Forseti.Windows.Growl
{
    class GrowlHelper
    {
        public const string GrowlType = "TestRunResult";
        public static void simpleGrowl(string title, string message = "")
        {
            GrowlConnector simpleGrowl = new GrowlConnector();
            global::Growl.Connector.Application thisApp = new global::Growl.Connector.Application("Forseti");
            NotificationType simpleGrowlType = new NotificationType(GrowlType, "JavaScript test/spec result");
            simpleGrowl.Register(thisApp, new NotificationType[] { simpleGrowlType });
            Notification myGrowl = new Notification("Forseti", GrowlType, title, title, message);
            simpleGrowl.Notify(myGrowl);
        }
    }
}
