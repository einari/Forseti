using System.Collections.Generic;
using Forseti.Files;
using System.IO;

namespace Forseti.Suites
{
    public class Description
    {
		List<IFile> _dependencies = new List<IFile>();
        List<Case> _cases = new List<Case>();

        public Suite Suite { get; set; }
        public string Name { get; private set; }
        public IFile File { get; set; }

        public Description(IFile file)
        {
            Name = Path.GetFileNameWithoutExtension(file.Filename);
            File = file;
            //ResetCasesForReporting();
        }
		
		public IEnumerable<IFile> Dependencies { get { return _dependencies; } }
        public IEnumerable<Case> Cases 
        { 
            get { return _cases; }
            set
            {
                _cases.Clear();
                _cases.AddRange(value);
            }
        }

        public void AddCase(Case @case)
        {
            @case.Description = this;
            _cases.Add(@case);
        }

        public  void ResetCasesForReporting()
        {
            var dummyCaseForReportingPurposes = Case.DummyCase;
            _cases.Clear();

            AddCase(dummyCaseForReportingPurposes);
        }

    }
}
