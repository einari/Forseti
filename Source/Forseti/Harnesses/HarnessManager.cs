using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forseti.Files;
using Forseti.Pages;
using Forseti.Scripting;
using Forseti.Suites;
using Forseti.Extensions;

namespace Forseti.Harnesses
{
    public class HarnessManager : IHarnessManager
    {
        List<Harness> _harnesses = new List<Harness>();
        IScriptEngine _scriptEngine;
        IPageGenerator _pageGenerator;
		IFileSystem _fileSystem;
		IFileSystemWatcher _fileSystemWatcher;
        IHarnessChangeManager _harnessChangeManager;

        public HarnessManager(
			IScriptEngine scriptEngine, 
			IPageGenerator pageGenerator, 
			IFileSystem fileSystem,
			IFileSystemWatcher fileSystemWatcher,
            IHarnessChangeManager harnessChangeManager)
        {
            _scriptEngine = scriptEngine;
            _pageGenerator = pageGenerator;
			_fileSystem = fileSystem;
			_fileSystemWatcher = fileSystemWatcher;
			_fileSystemWatcher.SubscribeToChanges(FileChanged);
            _harnessChangeManager = harnessChangeManager;
        }
		
		void FileChanged(FileChange change, IFile file)
		{
            foreach (var harness in _harnesses)
            {
				IEnumerable<Suite> affectedSuites = null;
				
                if (harness.IsSystem(file) || harness.IsDescription(file))
                {
					if( change == FileChange.Added ) 
					{
						affectedSuites = harness.HandleFiles(new[] {file});
					}
                    else if (change == FileChange.Deleted)
                    {
                        var suitesToRemove = new List<Suite>();
                        foreach (var suite in harness.Suites)
                        {
                            if (suite.SystemFile.FullPath == file.FullPath)
                                suitesToRemove.Add(suite);
                            else
                            {
                                var descriptionsToRemove = new List<Description>();
                                foreach (var description in suite.Descriptions)
                                {
                                    if (description.File.FullPath == file.FullPath)
                                        descriptionsToRemove.Add(description);
                                }
                                suite.RemoveDescriptions(descriptionsToRemove);
                            }
                        }
                        harness.RemoveSuites(suitesToRemove);
                    }
                    else
                    {
                        foreach (var suite in harness.Suites)
                        {
                            var runSuite = false;
                            if (suite.SystemFile.FullPath == file.FullPath)
                            {
                                runSuite = true;
                            }

                            foreach (var description in suite.Descriptions)
                            {
                                if (description.File.FullPath == file.FullPath)
                                    runSuite = true;
                            }

                            if (runSuite)
                                affectedSuites = new[] { suite };
                        }
                    }
                }

                if (affectedSuites != null)
                {
                    foreach (var suite in affectedSuites)
                    {
                        var now = DateTime.Now;
                        Execute(harness, new[] { suite });
                        suite.LastRun = now;
                    }
                }
            }
		}


		public void Add (Harness harness)
		{
			// Subscribe to changes from the filesystem
			// When a change occurs - walk through existing suites in the harness and see if there was a change to one of them - if so, rerun it
			
			// If a change is to a non existing suite, filter through the configuration : 
			//    - Does it match the regex of the SystemsSearchPath and does not match the DescriptionsSearchPath - add it as a system
			//    - Does it match the regex of the DescriptionSearchPath and not the SystemsSearchPath - look for the system in existing list, if exist add it, if not - try to find it in the filesystem based on configuration - add it if so
			
			// If the change is a delete - walk through existing suites in the harness and see some one is affected and just update the suite or remove the suite if the system was removed
			
			var allFiles = _fileSystem.GetAllFiles("*.js");
            harness.HandleFiles(allFiles);
            _harnesses.Add(harness);
		}

		public void Reset ()
		{
			throw new NotImplementedException ();
		}
        

        public HarnessResult Execute(Harness harness, IEnumerable<Suite> suites)
        {
			Console.WriteLine("<--- Run Suite(s) for {0} on {1} framework --->", harness.Name, harness.Framework.Name);

            var result = new HarnessResult(harness);
            result.StartTime = DateTime.Now;
			if( suites.Count() == 0 ) 
			{
				Console.WriteLine ("No suites");
                _harnessChangeManager.NotifyChange(result, HarnessChangeType.RunComplete);
				return null;
			}
            suites.ForEach(s => 
                            {
                                s = PrepareSuiteForReporting(s);
                                result.AddAffectedSuite(s); 
                            });
			
            var cases = new List<Case>();
            var timeBefore = DateTime.Now;
            
            foreach (var suite in suites)
            {
                foreach (var description in suite.Descriptions)
                    cases.AddRange(description.Cases);

                harness.Cases = cases;
            }
			
			if( harness.Cases.Count () == 0 )
			{
				Console.WriteLine("no cases");
			} else 
			{
	            var page = _pageGenerator.GenerateFrom(harness);

                // Todo: this is no good - just a temporary testing thing....
                HarnessCaseReporter.HarnessResult = result;
	            _scriptEngine.Execute(page);
			}

            result.EndTime = DateTime.Now;
            var delta = DateTime.Now.Subtract(timeBefore);

            
            _harnessChangeManager.NotifyChange(result, HarnessChangeType.RunComplete);

            Console.WriteLine("<--- Took {0} seconds --->\r\n", delta.TotalSeconds);

            return result;
        }

        private Suite PrepareSuiteForReporting(Suite s)
        {
            s.Descriptions.ForEach(d => d.ResetCasesForReporting());
            return s;
        }

        public IEnumerable<HarnessResult> Run()
        {
            var results = new List<HarnessResult>();
            foreach (var harness in _harnesses)
                results.Add(Execute(harness, harness.Suites));

            return results;
        }


        public IEnumerable<Harness> Harnesses { get { return _harnesses; } }
    }
}
