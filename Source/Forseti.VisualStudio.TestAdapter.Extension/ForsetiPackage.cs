using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace Forseti.VisualStudio.TestAdapter.Extension
{

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Guid("991D15C2-F5ED-4EF3-8785-6E3732ED4CA1")]
    public class ForsetiSettings : DialogPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        [Browsable(true)]
        [Category("Something")]
        [DisplayName("Whatevva")]
        [Description("Something that does something")]
        public string Something { get; set; }
    }

    [PackageRegistration(UseManagedResourcesOnly=true)]
    [ProvideOptionPage(typeof(ForsetiSettings), "Forseti", "Forseti Settings", 115, 116, true)]
    [InstalledProductRegistration("#110","#112","1.0.0", IconResourceID = 400)]
    [ProvideAutoLoad("ADFC4E64-0397-11D1-9F4E-00A0C911004F")]
    [ProvideAutoLoad("f1536ef8-92ec-443c-9ed7-fdadf150da82")]
    [Guid("3DE9481D-7EA6-4988-B1F4-CFCE96CB0502")]
    public class ForsetiPackage : Package
    {
        protected override void Initialize()
        {
           base.Initialize();

            
        }
    }
}
