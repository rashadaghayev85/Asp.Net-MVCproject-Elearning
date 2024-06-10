using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCproject_Elearning.Helpers;
using MVCproject_Elearning.Helpers.Extensions;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.Services.Interfaces;
using MVCproject_Elearning.ViewModels.Categories;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
    [Area("admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public CategoryController(ICategoryService categoryService,
                                  IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _env = env;
        }
        [HttpGet]
       
        public async Task<IActionResult> Index(int page = 1)
        {
            var category = await _categoryService.GetAllPaginateAsync(page, 4);

            var mappedDatas = _categoryService.GetMappedDatas(category);
            int totalPage = await GetPageCountAsync(4);

            Paginate<CategoryCourseVM> paginateDatas = new(mappedDatas, totalPage, page);

            return View(paginateDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _categoryService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }


        [HttpGet]
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existCategory = await _categoryService.ExistAsync(category.Name);
            if (existCategory)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }


            if (!category.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Input can accept only image format");
                return View();
            }
            if (!category.Image.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB ");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + category.Image.FileName;

            // return Content(fileName);

            string path = Path.Combine(_env.WebRootPath, "img", fileName);
            await category.Image.SaveFileToLocalAsync(path);


            await _categoryService.CreateAsync(new Category { Name = category.Name, Image = fileName });
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var category = await _categoryService.GetByIdAsync((int)id);
            if (category is null) return NotFound();
            await _categoryService.DeleteAsync(category);



            return RedirectToAction(nameof(Index));



        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            //            Category category = await _context.Categories.Include(m => m.Products).ThenInclude(m => m.ProductImages).FirstOrDefaultAsync(m => m.Id == id);

            Category category = await _categoryService.GetByIdWithCoursesAsync((int)id);
            return View(category);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var category = await _categoryService.GetByIdAsync((int)id);
            if (category is null) return NotFound();

            return View(new CategoryEditVM { Name = category.Name, Images = category.Image });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            var a = await _categoryService.GetByIdWithCoursesAsync((int)id);
            request.Images = a.Image;

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (request.NewImages is not null)
            {

                if (id is null) return BadRequest();

                if (await _categoryService.ExistExceptByIdAsync((int)id, request.Name))
                {
                    ModelState.AddModelError("Name", "This category already exist");
                       return View();
                }


                var category = await _categoryService.GetByIdAsync((int)id);

                if (category is null) return NotFound();

                //if (category.Name == request.Name)
                //{
                //    return RedirectToAction(nameof(Index));
                //}
                //category.Name = request.Name;



                if (request.NewImages is not null)
                {


                    if (!request.NewImages.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewImages", "Input can accept only image format");
                        return View(request);

                    }
                    if (!request.NewImages.CheckFileSize(500))
                    {
                        ModelState.AddModelError("NewImages", "Image size must be max 500 KB ");
                        return View(request);
                    }
                    string oldPath = _env.GenerateFilePath("img", request.NewImages.Name);
                    oldPath.DeleteFileFromLocal();
                    string newfileName = Guid.NewGuid().ToString() + "-" + request.NewImages.FileName;
                    string newPath = _env.GenerateFilePath("img", newfileName);

                    await request.NewImages.SaveFileToLocalAsync(newPath);





                    category.Image = newfileName;



                }





                if (request.Name is not null)
                {
                    category.Name = request.Name;
                }



                await _categoryService.EditAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {







                //if (!request.NewImages.CheckFileType("svg/"))
                //{
                //    ModelState.AddModelError("NewImages", "Input can accept only image format");
                //    category.Image = images;
                //    return View(request);

                //}

                var category = await _categoryService.GetByIdAsync((int)id);

                if (category is null) return NotFound();









                if (request.Name is not null)
                {
                    category.Name = request.Name;
                }
                await _categoryService.EditAsync();
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
