using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TRX.Reporting
{
    public interface ICanGeneratetTrxPart
    {
        XElement GenerateTrxPart();
    }
}
