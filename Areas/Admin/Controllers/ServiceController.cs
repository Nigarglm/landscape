using Landscape.Areas.ViewModels.Service;
using Landscape.DAL;
using Landscape.Models;
using Landscape.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Landscape.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ServiceController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.ToListAsync();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceVM serviceVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            bool result = await _context.Services.AnyAsync(s=>s.Name.ToLower().Trim()== serviceVM.Name.ToLower().Trim());
            if(result)
            {
                ModelState.AddModelError("Name", "Bu adli service artiq movcuddur");
                return View();
            }

            if (!serviceVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File tipi uygun deyil");
                return View();
            }

            if (!serviceVM.Photo.ValidateSize(2 * 1024))
            {
                ModelState.AddModelError("Photo", "File olcusu uygun deyil");
                return View();
            }

            string fileName = await serviceVM.Photo.CreateFileAsync(_env.WebRootPath, "img", "services");

            Service service = new Service
            {
                Name = serviceVM.Name,
                Description = serviceVM.Description,
                ImageUrl=fileName
            };

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Service existed = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (existed != null) return NotFound();
            UpdateServiceVM serviceVM = new UpdateServiceVM
            {
                Name = existed.Name,
                Description = existed.Description
            };
            return View(serviceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateServiceVM serviceVM, int id)
        {
            if(!ModelState.IsValid) return View(serviceVM);

            Service existed = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (existed == null) return NotFound();

            bool result = await _context.Services.AnyAsync(s=>s.Name == serviceVM.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adli service artiq movcuddur");
                return View();
            }

            if(serviceVM.Photo!=null)
            {
                if (!serviceVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File olcusu uygun deyil");
                    return View(existed);
                }
                if (!serviceVM.Photo.ValidateSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "File tipi uygun deyil");
                    return View(existed);
                }

                string NewImage = await serviceVM.Photo.CreateFileAsync(_env.WebRootPath, "img", "services");
                existed.ImageUrl.DeleteFile(_env.WebRootPath, "img", "services");
                existed.ImageUrl = NewImage;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if(id<=0) return BadRequest();
            Service service = await _context.Services.FirstOrDefaultAsync(s=>s.Id == id);
            if(service==null)
            {
                return NotFound();
            }

            service.ImageUrl.DeleteFile(_env.WebRootPath, "img", "services");
            _context.Services.Remove(service);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail()
        {
            List<Service> services = await _context.Services.ToListAsync();
            return View(services);
        }

    }
}
