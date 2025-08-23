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
    }
}
