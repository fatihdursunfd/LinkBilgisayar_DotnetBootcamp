using Assessment.Core.DTOs;
using Assessment.Core.Models;
using Assessment.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : CustomBaseController
    {
        private readonly IService<UserFile> _userFileService;

        public FileController(IService<UserFile> userFileService)
        {
            _userFileService = userFileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFiles()
        {
            var response = await _userFileService.GetAllAsync();
            return CreateActionResult(CustomResponseDto<IEnumerable<UserFile>>.Success(200, response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            var response = await _userFileService.GetByIdAsync(id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files/", response.FileName);
            var stream = new MemoryStream();

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                fs.CopyTo(stream);

            stream.Seek(0, SeekOrigin.Begin);

            return this.File(
                fileContents: stream.ToArray(),
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: response.FileName
            );
        }
    }
}
