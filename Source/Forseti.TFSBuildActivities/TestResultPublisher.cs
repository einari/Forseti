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
    public class TestResultPublisher
    {
        CodeActivityContext _context;
        IBuildDetail _buildDetails;
        string _teamProjectUrl;
        string _buildNumber;
        string _teamProjectName;

        public TestResultPublisher(CodeActivityContext context)
        {
            _context = context;
            GetBuildDetails(context);

        }

        private void GetBuildDetails(CodeActivityContext context)
        {
            _buildDetails = context.GetExtension<IBuildDetail>();
            _teamProjectUrl = _buildDetails.BuildServer.TeamProjectCollection.Uri.ToString();
            _buildNumber = _buildDetails.BuildNumber;
            _teamProjectName = _buildDetails.TeamProject;

        }

        public void PublishResultsFromPath(string trxFilePath)
        {
            _context.Log("TrxFilePath : {0}", trxFilePath);
            var filePath = Path.GetFullPath(trxFilePath);

            //if (!File.Exists(filePath))
                //throw new FileNotFoundException("Could not locate the file path", trxFilePath);

            var mstestPath = Environment.ExpandEnvironmentVariables(@"C:\Program Files (X86)\Microsoft Visual Studio 10.0\Common7\IDE\MSTest.exe");//("%VS100COMNTOOLS%\\..\\IDE\\MSTest.exe");
            _context.Log("MSTEST Path : {0}", mstestPath);

            var arguments = string.Format(@"/publish:{0} /publishbuild:{1} /platform:{2} /flavor:{3} /teamproject:{4} /publishresultsfile:{5}",
                                                    _teamProjectUrl, _buildNumber, "\"x86\"", "Debug", _teamProjectName, trxFilePath);
            _context.Log("MSTEST Arguments : {0}", arguments);

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
                    _context.Log(outputStream);
                }

                string errorStream = msTest.StandardError.ReadToEnd();
                if (errorStream.Length > 0)
                {
                    _context.Log(errorStream);
                }
                msTest.WaitForExit();

                _context.Log("Ran MSTEST for {0}", DateTime.Now.Subtract(startTime));
            }
        }
    }
}
