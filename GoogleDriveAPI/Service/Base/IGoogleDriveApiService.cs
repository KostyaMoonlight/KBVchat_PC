using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveAPI.Service.Base
{
    public interface IGoogleDriveApiService
    {
        IEnumerable<string> GetFilesIds();
        string UploadFile(string name, Stream stream);
        Stream DownloadFile(string id);
    }
}
