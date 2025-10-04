using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;
using RShop.BLL.Interfaces;
using RShop.DAL.DTO.Responses;

namespace RShop.BLL.Classes
{
    public class FileService:IFileService
    {
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                return fileName;
            }

            throw new Exception("File is null or empty");
        }

        public async Task<List<string>> UploadManyAsync(List<IFormFile> files)
        {
            var fileNames = new List<string>();
            foreach (var file in files)
            {
                if (file is not null) { 
                    var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);
                    using (var stream = File.OpenWrite(filePath)) { 
                        await file.CopyToAsync(stream);
                    }

                    fileNames.Add(filename);
                }
            }

            return fileNames;
        }
    }
}
