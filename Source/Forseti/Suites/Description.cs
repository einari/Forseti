using System.Collections.Generic;
using Forseti.Files;
using System.IO;
using System;

namespace Forseti.Suites
{
    public class Description : IComparable
    {
		List<IFile> _dependencies = new List<IFile>();

        List<IFile> _contexts = new List<IFile>();

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

        public IEnumerable<IFile> Contexts { get { return _contexts; } }

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

        public void AddContext(IFile context)
        {
            _contexts.Add(context);
        }

        public void RemoveContext(IFile context)
        {
            _contexts.Remove(context);
        }


        public  void ResetCasesForReporting()
        {
            var dummyCaseForReportingPurposes = Case.DummyCase;
            _cases.Clear();

            AddCase(dummyCaseForReportingPurposes);
        }


        public int CompareTo(object obj)
        {
            var other = obj as Description;
            return other.File.FullPath.CompareTo(File.FullPath);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Description;
            return File.FullPath.Equals(other.File.FullPath);
        }
    }
}
