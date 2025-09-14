using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TicketBooking.Web.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Web.Repositories.Implementations
{
    public class UtilityRepo : IUtilityRepo
    {
        private IWebHostEnvironment _env;
        private IHttpContextAccessor _contextAccessor;

        public UtilityRepo(IWebHostEnvironment env,
            IHttpContextAccessor contextAccessor)
        {
            _env = env;
            _contextAccessor = contextAccessor;
        }

        public Task DeleteFile(string filePath, string DirName)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                return Task.CompletedTask;
            }
            var filename = Path.GetFileName(filePath);
            var completeFilePath = Path.Combine(_env.WebRootPath, DirName, filename);
            if(File.Exists(completeFilePath))
            {
                File.Delete(completeFilePath);
            }
            return Task.CompletedTask;  

        }

        public async Task<string> EditFilePath(string DirName, IFormFile file, string fullPath)
        {
            await DeleteFile(fullPath, DirName);
            return await SaveImagePath(DirName, file);
        }

        public async Task<string> SaveImagePath(string DirName, IFormFile file)
        {
          string dir =  Path.Combine(_env.WebRootPath, DirName);
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var extension = Path.GetExtension(file.FileName);
            var filename = $"{Guid.NewGuid()} {extension}";
            string completeFilePath = Path.Combine(dir, filename);
            using(var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                await File.WriteAllBytesAsync(completeFilePath, content);
            }
            var basePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
            var fullPath = Path.Combine(basePath, DirName, filename).Replace("\\", "/");
            return fullPath;



        }
    }
}
