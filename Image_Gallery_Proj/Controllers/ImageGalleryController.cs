using Microsoft.AspNetCore.Mvc;

namespace Image_Gallery_Proj.Controllers
{
    public class ImageGalleryController : Controller
    {
        private readonly string rootpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploads");
        public IActionResult Index()
        {
            // create directory here
            bool dirExists = Directory.Exists(rootpath);

            if (!dirExists)
            {
                Directory.CreateDirectory(rootpath);
            }

            List<string> images = Directory.GetFiles(rootpath).Select(Path.GetFileName).ToList();
            return View(images);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if(file != null)
            {
                // Get file Nmae here
                var path = Path.Combine(rootpath, Guid.NewGuid() + Path.GetExtension(file.FileName));

                // Upload file directory
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                return RedirectToAction("Index");
            }

            return View();  
        }
    }
}
