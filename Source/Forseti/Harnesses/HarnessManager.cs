using System;
using System.Collections.Generic;
using System.IO;
using Forseti.Files;
using Forseti.Pages;
using Forseti.Scripting;
using Forseti.Suites;

namespace Forseti.Harnesses
{
    public class HarnessManager : IHarnessManager
    {
        IScriptEngine _scriptEngine;
        IPageGenerator _pageGenerator;
		IFileSystem _fileSystem;
		IFileSystemWatcher _fileSystemWatcher;


        public HarnessManager(
			IScriptEngine scriptEngine, 
			IPageGenerator pageGenerator, 
			IFileSystem fileSystem,
			IFileSystemWatcher fileSystemWatcher)
        {
            _scriptEngine = scriptEngine;
            _pageGenerator = pageGenerator;
			_fileSystem = fileSystem;
			_fileSystemWatcher = fileSystemWatcher;
			
			// Subscribe to changes from the filesystem
			// When a change occurs - walk through existing suites in the harness and see if there was a change to one of them - if so, rerun it
			
			// If a change is to a non existing suite, filter through the configuration : 
			//    - Does it match the regex of the SystemsSearchPath and does not match the DescriptionsSearchPath - add it as a system
			//    - Does it match the regex of the DescriptionSearchPath and not the SystemsSearchPath - look for the system in existing list, if exist add it, if not - try to find it in the filesystem based on configuration - add it if so
			
			// If the change is a delete - walk through existing suites in the harness and see some one is affected and just update the suite or remove the suite if the system was removed

            var currentDir = Directory.GetCurrentDirectory();

            var w = new System.IO.FileSystemWatcher(currentDir, "*.js");
            w.IncludeSubdirectories = true;
            w.NotifyFilter = NotifyFilters.LastWrite;
            w.Changed += new FileSystemEventHandler(w_Changed);
            w.EnableRaisingEvents = true;
            
        }

        DateTime _lastTrigger = DateTime.Now;
        IEnumerable<Suite> _currentSuites;

        Dictionary<Suite, DateTime> _lastTriggered = new Dictionary<Suite, DateTime>();

        void w_Changed(object sender, FileSystemEventArgs e)
        {
            var name = Path.GetFileName(e.Name).ToLowerInvariant();
            foreach (var suite in _currentSuites)
            {
                var suiteChanged = false;

                var systemFile = Path.GetFileName(suite.SystemFile).ToLowerInvariant();
                if (systemFile == name)
                    suiteChanged = true;
                else
                {
                    foreach (var description in suite.Descriptions)
                    {
                        var suiteDescriptionFile = Path.GetFileName(description.File).ToLowerInvariant();
                        if (suiteDescriptionFile == name)
                            suiteChanged = true;

                    }
                }

                if (suiteChanged)
                {
                    var delta = _lastTriggered.ContainsKey(suite) ?
                        DateTime.Now.Subtract(_lastTriggered[suite]) :
                        TimeSpan.MaxValue;
                    if( delta.Seconds >= 1 ) 
                        Execute(new[] { suite });

                    _lastTriggered[suite] = DateTime.Now;
                }
            }
        }

		public void Add (Harness harness)
		{
			throw new NotImplementedException ();
		}

		public void Reset ()
		{
			throw new NotImplementedException ();
		}
        


        public Harness Execute(IEnumerable<Suite> suites)
        {
            if( _currentSuites == null ) 
                _currentSuites = suites;


            var harness = new Harness();
            var cases = new List<Case>();

            var timeBefore = DateTime.Now;
            Console.WriteLine("<--- Run Suite(s) --->");
            foreach (var suite in suites)
            {
                foreach (var description in suite.Descriptions)
                    cases.AddRange(description.Cases);

                harness.Cases = cases;

            }
            var page = _pageGenerator.GenerateFrom(harness);

            

            _scriptEngine.Execute(page);

            var delta = DateTime.Now.Subtract(timeBefore);

            Console.WriteLine("<--- Took {0} seconds --->\n", delta.TotalSeconds);
            return harness;
        }



    }
}
