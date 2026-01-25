using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{

    /// <summary>
    /// Interface for file service
    /// </summary>
    public interface IFileService
    {
        Task<string> UploadResumeAsync(Stream fileStream, string fileName);
        Task<string> UploadOfferLetterAsync(Stream fileStream, string fileName);
        Task<bool> DeleteFileAsync(string filePath);
        Task<Stream> DownloadFileAsync(string filePath);
    }
}
