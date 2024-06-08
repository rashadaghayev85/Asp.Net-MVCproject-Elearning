using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Helpers.Extensions;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.Abouts;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AboutController : Controller
    {

        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _env;
        public AboutController(IAboutService aboutService,
                                  
                                   IWebHostEnvironment env)
        {
            _aboutService = aboutService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _aboutService.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Input can accept only image format");
                return View();
            }
            if (!request.Image.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB ");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            // return Content(fileName);

            string path = Path.Combine(_env.WebRootPath, "img", fileName);
            await request.Image.SaveFileToLocalAsync(path);


            bool existBlog = await _aboutService.ExistAsync(request.Title);
            if (existBlog)
            {
                ModelState.AddModelError("Title", "This title already exist");
                return View();
            }
            await _aboutService.CreateAsync(new About { Title = request.Title, Description = request.Description, Image = fileName });
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var deleteAbout = await _aboutService.GetByIdAsync((int)id);
            if (deleteAbout is null) return NotFound();

            string path = _env.GenerateFilePath("img", deleteAbout.Image);


            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            _aboutService.DeleteAsync(deleteAbout);
            return RedirectToAction(nameof(Index));






        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            About about = await _aboutService.GetByIdAsync((int)id);
            return View(about);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var about = await _aboutService.GetByIdAsync((int)id);
            if (about == null) return NotFound();
            //return View();
            return View(new AboutEditVM { Title = about.Title, Description = about.Description, Image = about.Image });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {
            if (id == null) return BadRequest();
            var about = await _aboutService.GetByIdAsync((int)id);
            if (about == null) return NotFound();
            if (request.NewImage is null) return RedirectToAction(nameof(Index));

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Input can accept only image format");
                request.Image = about.Image;
                return View(request);

            }
            if (!request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200 KB ");
                request.Image = about.Image;
                return View(request);
            }
            string oldPath = _env.GenerateFilePath("img", about.Image);
            oldPath.DeleteFileFromLocal();
            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
            string newPath = _env.GenerateFilePath("img", fileName);

            await request.NewImage.SaveFileToLocalAsync(newPath);
            if (request.Title is not null)
            {
                about.Title = request.Title;
            }
            if (request.Description is not null)
            {
                about.Description = request.Description;
            }
            if (fileName is not null)
            {
                about.Image = fileName;
            }

            await _aboutService.EditAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
