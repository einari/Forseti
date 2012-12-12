using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Forseti.TFSBuildActivities
{
    public class TestRunner
    {
        const string ForsetiTrxExecutable = "Forseti.TRX.exe";

        string _forsetiExecutablePath;
        string _forsetiArguments;
        string _forsetiWorkingDirectory;

       Action<string> _logger; 

        public TestRunner(string forsetiConfiguration, string trxOutput, string computerName, string localUser, string tfsUser) 
        {
            _logger = (s) => Console.WriteLine(s);

            var executingAssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _forsetiExecutablePath = Path.Combine(executingAssemblyFolder, ForsetiTrxExecutable);
            _forsetiArguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\"",
                                                forsetiConfiguration,
                                                trxOutput,
                                                computerName,
                                                localUser,
                                                tfsUser);

            var forsetiConfigfile = new FileInfo(forsetiConfiguration);
            if (forsetiConfigfile.Exists)
                _forsetiWorkingDirectory = forsetiConfigfile.DirectoryName;

         }

        public void LogTo(Action<string> logOutputTo) 
        {
            _logger = logOutputTo;
        }

        internal bool RunTests()
        {
            Log(string.Format("{0} {1}",_forsetiExecutablePath,_forsetiArguments));

            bool allTestsPassed = false;
            using (var forsetiTrx = new Process())
            {
                var startTime = DateTime.Now;

                forsetiTrx.StartInfo.FileName = _forsetiExecutablePath;
                forsetiTrx.StartInfo.Arguments = _forsetiArguments;
                forsetiTrx.StartInfo.WorkingDirectory = _forsetiWorkingDirectory;

                forsetiTrx.StartInfo.UseShellExecute = false;
                forsetiTrx.StartInfo.RedirectStandardOutput = true;
                forsetiTrx.StartInfo.RedirectStandardError = true;

                forsetiTrx.Start();

                string outputStream = forsetiTrx.StandardOutput.ReadToEnd();
                if (outputStream.Length > 0)
                {
                    Log(outputStream);
                }

                string errorStream = forsetiTrx.StandardError.ReadToEnd();
                if (errorStream.Length > 0)
                {
                    Log(errorStream);
                }
                forsetiTrx.WaitForExit();

                allTestsPassed = forsetiTrx.ExitCode == 0 ? true : false;

            }

            return allTestsPassed;
        }
    }
}
