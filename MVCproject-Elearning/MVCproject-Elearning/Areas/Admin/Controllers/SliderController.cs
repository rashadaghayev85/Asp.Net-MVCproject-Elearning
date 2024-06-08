using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject_Elearning.Data;
using MVCproject_Elearning.Helpers.Extensions;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.Sliders;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService, IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var slider = await _sliderService.GetAllAsync();

            List<SliderVM> sliders = slider.Select(m => new SliderVM { Id = m.Id, Image = m.Image, Title = m.Title, Description = m.Description }).ToList();

            return View(sliders);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
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
            await _sliderService.CreateAsync(new Slider { Image = fileName, Title = request.Title, Description = request.Description });



            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var deleteSlider = await _sliderService.GetByIdAsync((int)id);
            if (deleteSlider == null) return NotFound();
            string path = _env.GenerateFilePath("img", deleteSlider.Image);

            path.DeleteFileFromLocal();


            _sliderService.DeleteAsync(deleteSlider);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var slider = await _sliderService.GetByIdAsync((int)id);
            if (slider is null) return NotFound();


            SliderDetailVM response = new()
            {
                Title = slider.Title,
                Description = slider.Description,

                Image = slider.Image,
            };
            return View(response);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var slider = await _sliderService.GetByIdAsync((int)id);
            if (slider == null) return NotFound();
            return View(new SliderEditVM { Image = slider.Image });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id == null) return BadRequest();
            var slider = await _sliderService.GetByIdAsync((int)id);
            if (slider == null) return NotFound();
            if (request.NewImage is null)
            {
                if (request.Title is not null)
                {
                    slider.Title = request.Title;
                }
                if (request.Description is not null)
                {
                    slider.Description = request.Description;
                }

                await _sliderService.EditAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Input can accept only image format");
                request.Image = slider.Image;
                return View(request);

            }
            if (!request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200 KB ");
                request.Image = slider.Image;
                return View(request);
            }
            string oldPath = _env.GenerateFilePath("img", slider.Image);
            oldPath.DeleteFileFromLocal();
            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
            string newPath = _env.GenerateFilePath("img", fileName);
            await request.NewImage.SaveFileToLocalAsync(newPath);
            slider.Image = fileName;

            if (request.Title is not null)
            {
                slider.Title = request.Title;
            }
            if (request.Description is not null)
            {
                slider.Description = request.Description;
            }

            await _sliderService.EditAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
