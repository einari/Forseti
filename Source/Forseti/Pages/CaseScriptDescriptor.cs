using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.Pages
{
    public class CaseScriptDescriptor
    {
        public IEnumerable<string> CaseDependencies { get; set; }
        public string Case { get; set; }

        public CaseScriptDescriptor() { 
            CaseDependencies = new string[]{};
        }

        public static implicit operator CaseScriptDescriptor(string script)
        {

            return new CaseScriptDescriptor { Case = script };

        }
        public static implicit operator string(CaseScriptDescriptor descriptor)
        {

            return descriptor.Case;

        }
    }
}
