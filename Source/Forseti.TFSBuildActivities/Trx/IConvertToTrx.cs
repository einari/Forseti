using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TFSBuildActivities.Trx
{
    public interface IConvertToTrx
    {
        XElement ConvertToTrxNode();
    }
}
