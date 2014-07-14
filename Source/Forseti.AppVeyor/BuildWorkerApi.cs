//File retrofitted from: https://github.com/AppVeyor/xunit/commit/dbb90363191ef900716bf97b4427139adcda36fe
   

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Forseti.AppVeyor
{
    public class BuildWorkerApi : Forseti.AppVeyor.IBuildWorkerApi 
    {
        private static string _apiUrl = null;
        
        public BuildWorkerApi() 
        { 
        
        }

        public void AddTest(string testName, string testFramework, string fileName,
            string outcome, long? durationMilliseconds, string errorMessage, string errorStackTrace, string stdOut, string stdErr)
        {
            if (GetApiUrl() == null)
            {
                return;
            }

            var body = new AddUpdateTestRequest
            {
                TestName = testName,
                TestFramework = testFramework,
                FileName = fileName,
                Outcome = outcome,
                DurationMilliseconds = durationMilliseconds,
                ErrorMessage = errorMessage,
                ErrorStackTrace = errorStackTrace,
                StdOut = TrimStdOut(stdOut),
                StdErr = TrimStdOut(stdErr)
            };

            try
            {
                using (WebClient wc = GetClient())
                {
                    wc.UploadData("api/tests", "POST", Json(body));
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error communicating AppVeyor Build Worker API: " + ex.Message);
            }
        }

        public void UpdateTest(string testName, string testFramework, string fileName,
            string outcome, long? durationMilliseconds, string errorMessage, string errorStackTrace, string stdOut, string stdErr)
        {
            if (GetApiUrl() == null)
            {
                return;
            }

            var body = new AddUpdateTestRequest
            {
                TestName = testName,
                TestFramework = testFramework,
                FileName = fileName,
                Outcome = outcome,
                DurationMilliseconds = durationMilliseconds,
                ErrorMessage = errorMessage,
                ErrorStackTrace = errorStackTrace,
                StdOut = TrimStdOut(stdOut),
                StdErr = TrimStdOut(stdErr)
            };

            try
            {
                using (WebClient wc = GetClient())
                {
                    wc.UploadData("api/tests", "PUT", Json(body));
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error communicating AppVeyor Build Worker API: " + ex.Message);
            }
        }

        private string TrimStdOut(string str)
        {
            int maxLength = 4096;

            if (str == null)
            {
                return null;
            }

            return (str.Length > maxLength) ? str.Substring(0, maxLength) : str;
        }

        private byte[] Json(object data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            return Encoding.UTF8.GetBytes(json);
        }

        private WebClient GetClient()
        {
            WebClient wc = new WebClient();
            wc.BaseAddress = GetApiUrl();
            wc.Headers["Accept"] = "application/json";
            wc.Headers["Content-type"] = "application/json";
            return wc;
        }

        private string GetApiUrl()
        {
            // get API URL from registry
            if (_apiUrl == null)
            {
                _apiUrl = Environment.GetEnvironmentVariable("APPVEYOR_API_URL");

                if (_apiUrl != null)
                {
                    _apiUrl = _apiUrl.TrimEnd('/') + "/";
                }
            }

            return _apiUrl;
        }

        public class WebDownload : WebClient
        {
            /// <summary>
            /// Time in milliseconds
            /// </summary>
            public int Timeout { get; set; }

            public WebDownload() : this(60000) { }

            public WebDownload(int timeout)
            {
                this.Timeout = timeout;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                if (request != null)
                {
                    request.Timeout = this.Timeout;
                }
                return request;
            }
        }

        public class AddUpdateTestRequest
        {
            public string TestName { get; set; }
            public string FileName { get; set; }
            public string TestFramework { get; set; }
            public string Outcome { get; set; }
            public long? DurationMilliseconds { get; set; }
            public string ErrorMessage { get; set; }
            public string ErrorStackTrace { get; set; }
            public string StdOut { get; set; }
            public string StdErr { get; set; }
        }
    }
     
}
