using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore;
using BorroApp.Extensions;
using BorroApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace BorroApp.Controller
{
    public class FileUploadController : ControllerBase
    {
        private readonly BorroDbContext _context;
        private readonly BlobContainerClient _blobContainerClient;
        public FileUploadController(BorroDbContext context, BlobContainerClient blobContainerClient)
        {
            _context = context;
            _blobContainerClient = blobContainerClient;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(CreateFile createFile)
        {
            var extension = Path.GetExtension(createFile.Picture.FileName);
            string whichType = createFile.Type;

            if (whichType != "post" && whichType != "userInfo")
            {
                return BadRequest();
            }

            if (createFile.Type == "post")
            {
                var stuff = await _context.Post.FindAsync(createFile.Id);
                var blobClient = _blobContainerClient.GetBlobClient($"{whichType}_{stuff.Id}{extension}");
                await using var data = createFile.Picture.OpenReadStream();

                await blobClient.UploadAsync(data, new BlobHttpHeaders
                {
                    ContentType = createFile.Picture.ContentType
                });

                stuff.Image = blobClient.Uri.ToString();
                await _context.SaveChangesAsync();
            }

            if (createFile.Type == "userInfo")
            {
                var stuff = await _context.UserInfo.FindAsync(createFile.Id);
                var blobClient = _blobContainerClient.GetBlobClient($"{whichType}_{stuff.Id}{extension}");
                await using var data = createFile.Picture.OpenReadStream();

                await blobClient.UploadAsync(data, new BlobHttpHeaders
                {
                    ContentType = createFile.Picture.ContentType
                });

                stuff.ProfileImage = blobClient.Uri.ToString();
                await _context.SaveChangesAsync();
            }
            return Created();
        }
    }

    public class CreateFile
    {
        public int Id { get; set; }
        public IFormFile Picture { get; set; }
        public string Type { get; set; }
    }
}
