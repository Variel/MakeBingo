using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MakeBingo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MakeBingo.Controllers
{
    public class ResultController : Controller
    {
        private readonly DatabaseContext _database;
        private readonly CloudBlobClient _blobClient;

        public ResultController(DatabaseContext database, IConfiguration config)
        {
            _database = database;

            var storageAccount = CloudStorageAccount.Parse(config.GetConnectionString("AzureStorage"));
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<IActionResult> Detail(string id)
        {
            var result = await _database.Results
                .Include(r => r.Board)
                    .ThenInclude(b => b.Parent)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (result == null)
                return NotFound();

            return View(result);
        }

        public async Task<IActionResult> Image(string id)
        {
            var result = await _database.Results
                .Include(r => r.Board)
                .ThenInclude(b => b.Parent)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (result == null)
                return NotFound();

            return View(result);
        }

        [Route("api/results/{id}/uploadImage")]
        [HttpPost]
        public async Task<IActionResult> UploadImage(string id, IFormFile image)
        {
            var container = _blobClient.GetContainerReference("images");
            var blob = container.GetBlockBlobReference($"{id}{Path.GetExtension(image.FileName)}");
            blob.Properties.ContentType = image.ContentType;

            await blob.UploadFromStreamAsync(image.OpenReadStream());

            return Created(blob.Uri, null);
        }
    }
}