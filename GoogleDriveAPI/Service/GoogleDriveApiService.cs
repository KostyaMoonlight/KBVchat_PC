using GoogleDriveAPI.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;

namespace GoogleDriveAPI.Service
{
    public class GoogleDriveApiService
        : IGoogleDriveApiService
    {
        private string[] scopes = { DriveService.Scope.Drive };
        private string ApplicationName = "messengerfilesdb";
        private DriveService _service = null;

        public GoogleDriveApiService()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-messenger.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            _service = service;
        }

        public Stream DownloadFile(string id)
        {
            Stream stream = new MemoryStream();
            var getRequest = _service.Files.Get(id);
            getRequest.Download(stream);
            getRequest.Execute();

            return stream;
        }

        public IEnumerable<string> GetFilesIds()
        {
            FilesResource.ListRequest listRequest = _service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            List<string> ids = null;
            if (files != null && files.Count > 0)
            {
                ids = new List<string>();
                foreach (var f in files)
                {
                    ids.Add(f.Id);
                }
            }

            return ids;
        }

        public string UploadFile(string name, Stream stream)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name
            };

            FilesResource.CreateMediaUpload request;
            request = _service.Files.Create(
                fileMetadata, stream, "");
            request.Fields = "id";
            request.Upload();

            var file = request.ResponseBody;
            return file.Id;

        }
    }
}
