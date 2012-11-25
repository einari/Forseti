using System.IO;
using System.Net;

namespace Forseti.TFSBuildActivities
{
    /// <summary>
    /// Uploads files to TFS 2010.
    /// </summary>
    internal static class FileUploader
    {
        private const string Boundary = "--------------------------8e5m2D6l5Q4h6";
        private static readonly string Url;

        static FileUploader()
        {
            Url = "http://tfs:8080/tfs/1.0/AttachmentUpload.ashx";
        }

        internal static void UploadFile(string filename, byte[] file, string projectName, int testRunId, int attachmentId, int testResultId = 0)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(Url);

            webRequest.UseDefaultCredentials = true;
            webRequest.Method = "POST";
            webRequest.ContentType = "multipart/form-data; boundary=" + Boundary;

            byte[] postData;
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    WriteFormData(writer, "ProjectName", projectName);
                    WriteFormData(writer, "TestRunId", testRunId);
                    WriteFormData(writer, "TestResultId", testResultId);
                    WriteFormData(writer, "AttachmentId", attachmentId);
                    WriteFormData(writer, "UncompressedLength", file.Length);
                    WriteFormData(writer, "DefaultAfnStripFlag", 0);
                    WriteFormData(writer, "Range", "bytes=0-" + file.Length + "/" + file.Length);
                    WriteFile(memoryStream, writer, filename, file);
                    writer.WriteLine();
                    writer.Write("--");
                    writer.Write(Boundary);
                    writer.WriteLine("--");
                    writer.Flush();
                }
                postData = memoryStream.GetBuffer();
            }

            webRequest.ContentLength = postData.Length;

            // write the post data
            using (Stream postStream = webRequest.GetRequestStream())
            {
                postStream.Write(postData, 0, postData.Length);
            }

            webRequest.GetResponse().Close();
        }

        private static void WriteFormData<T>(TextWriter writer, string name, T value)
        {
            writer.Write("--");
            writer.WriteLine(Boundary);
            writer.Write("Content-Disposition: form-data; name=\"");
            writer.Write(name);
            writer.WriteLine("\"");
            writer.WriteLine();
            writer.WriteLine(value);
        }

        private static void WriteFile(Stream stream, TextWriter writer, string filename, byte[] file)
        {
            writer.Write("--");
            writer.WriteLine(Boundary);
            writer.Write("Content-Disposition: form-data; name=\"File\" filename=\"");
            writer.Write(filename);
            writer.WriteLine("\"");
            writer.WriteLine("Content-Type: application/octet-stream");
            writer.WriteLine();
            writer.Flush();
            stream.Write(file, 0, file.Length);
        }
    }
}
