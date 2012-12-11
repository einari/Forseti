using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Microsoft.TeamFoundation.Build.Client;
using System.IO;
using System.Diagnostics;

namespace Forseti.TFSBuildActivities
{
    public class TfsResultPublisher
    {
        string _teamProjectUrl;
        string _buildNumber;
        string _teamProjectName;

        public Action<string> Log { get; set; }

        public TfsResultPublisher(string teamProjectUrl, string buildNumber, string teamProjectName)
        {
            _teamProjectUrl = teamProjectUrl;
            _buildNumber = buildNumber;
            _teamProjectName = teamProjectName;

            Log = (s) => Console.WriteLine(s); 
        }

        public void PublishResultsFromPath(string trxFilePath)
        {
            Log(string.Format("TrxFilePath : {0}", trxFilePath));
            var filePath = Path.GetFullPath(trxFilePath);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Could not locate the trx to be published to the build at following path : {0}", trxFilePath);

            var mstestPath = Environment.ExpandEnvironmentVariables(@"C:\Program Files (X86)\Microsoft Visual Studio 10.0\Common7\IDE\MSTest.exe");//("%VS100COMNTOOLS%\\..\\IDE\\MSTest.exe");
            Log(string.Format("MSTEST Path : {0}", mstestPath));

            var arguments = string.Format(@"/publish:{0} /publishbuild:{1} /platform:{2} /flavor:{3} /teamproject:{4} /publishresultsfile:{5}",
                                                    _teamProjectUrl, _buildNumber, "\"x86\"", "Debug", _teamProjectName, trxFilePath);
           Log(string.Format("MSTEST Arguments : {0}", arguments));

            // Command-Line Options For puvlishing Test Results: http://msdn.microsoft.com/en-us/library/ms243151.aspx
            using (var msTest = new Process())
            {
                var startTime = DateTime.Now;

                msTest.StartInfo.FileName = mstestPath;
                msTest.StartInfo.Arguments = arguments;

                msTest.StartInfo.UseShellExecute = false;
                msTest.StartInfo.RedirectStandardOutput = true;
                msTest.StartInfo.RedirectStandardError = true;

                msTest.Start();

                string outputStream = msTest.StandardOutput.ReadToEnd();
                if (outputStream.Length > 0)
                {
                    Log(outputStream);
                }

                string errorStream = msTest.StandardError.ReadToEnd();
                if (errorStream.Length > 0)
                {
                    Log(errorStream);
                }
                msTest.WaitForExit();

               Log(string.Format("Ran MSTEST for {0}", DateTime.Now.Subtract(startTime)));
            }
        }
    }
}
