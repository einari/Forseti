using System;
using System.Collections.Generic;
using System.IO;

namespace Forseti.Pages
{
    public class Page : IDisposable
    {
        List<string> _runnerDependencies = new List<string>();

        public string RootPath { get; set; }
        public string Filename { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> RunnerDependencies { get { return _runnerDependencies; } }

        public void AddRunnerDependency(string file, string content)
        {
            _runnerDependencies.Add(file);
            File.WriteAllText(RootPath + file, content);
        }

        public void Dispose()
        {
            //_runnerDependencies.ForEach(File.Delete);
            //File.Delete(RootPath + Filename);
        }
    }
}
