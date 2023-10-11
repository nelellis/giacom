using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.Services
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file);
        void DeleteFile(string filePath);
    }
    public class FileService: IFileService
    {
        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentNullException("No file uploaded.");
            if (!file.FileName.ToLower().EndsWith(".csv"))
            {
                throw new ArgumentException("Invalid file uploaded");
            }
            var uniqueFileName = $"{Guid.NewGuid().ToString()}.csv";
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // Combine the folder path and the unique file name
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //Saving file, so tthat in future we can move the file processing to microservice using queue
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }

        public void DeleteFile(string filePath)
        {
            if(File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
