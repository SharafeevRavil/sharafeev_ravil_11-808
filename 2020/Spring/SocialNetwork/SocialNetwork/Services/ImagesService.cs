using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class ImagesService
    {
        private readonly IWebHostEnvironment _env;

        public ImagesService(IWebHostEnvironment env)
        {
            _env = env;
        }

        private void EnsureImagesFolderExists()
        {
            var imagesDirInfo = new DirectoryInfo(ImagesDirectoryPath);
            if (!imagesDirInfo.Exists)
            {
                imagesDirInfo.Create();
            }
        }

        private string ImagesDirectoryPath => _env.WebRootPath + "/Images/";

        public async Task<string> LoadImageAsync(IFormFile imageFile)
        {
            EnsureImagesFolderExists();
            string imageName = Guid.NewGuid() + new FileInfo(imageFile.FileName).Extension;
            await using var fileStream = new FileStream(ImagesDirectoryPath + imageName, FileMode.Create);
            await imageFile.CopyToAsync(fileStream);

            return imageName;
        }
    }
}