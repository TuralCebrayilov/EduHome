using EduHome.DAL;
using EduHome.Helper;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Extensions = EduHome.Helper.Extensions;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly AppDbContext _Db;
        private readonly  IWebHostEnvironment _env ;
        public SlidersController(AppDbContext Db, IWebHostEnvironment env )
        {
            _Db = Db;
            _env= env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _Db.Sliders.ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            #region Save Image
           

            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "Image can't be null!!");
                return View();
            }
            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select image type");
                return View();
            }
            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "max 1mb !!");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath, "img", "slider");
            slider.Image = await slider.Photo.SaveFileAsync(folder);
            #endregion

            await _Db.Sliders.AddAsync(slider);
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

          public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider _Dbslider= await _Db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (_Dbslider == null)
            {
                return BadRequest();
            }
            if (_Dbslider.IsDeactive)
            {
                _Dbslider.IsDeactive = false;
            }
            else
            {
                _Dbslider.IsDeactive = true;
            }
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider _Dbslider = await _Db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (_Dbslider == null)
            {
                return BadRequest();
            }
            return View(_Dbslider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider _Dbslider = await _Db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (_Dbslider == null)
            {
                return BadRequest();
            }
            #region Save Image

            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please select image type");
                    return View();
                }

                if (slider.Photo == null)
                {
                    ModelState.AddModelError("Photo", "max 1mb !!");
                    return View();
                }

                string folder = Path.Combine(_env.WebRootPath, "img", "slider");
                slider.Image = await slider.Photo.SaveFileAsync(folder);
                string path = Path.Combine(_env.WebRootPath, folder, _Dbslider.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                _Dbslider.Image = slider.Image;
            }

            #endregion



            _Dbslider.Title = slider.Title;
            _Dbslider.Description = slider.Description;
            _Db.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {

            Slider slider = await _Db.Sliders.FindAsync(id);


            //_Dbservice.Name = service.Name;
            //_Dbservice.Description = service.Description;

            return View(slider);
        }
    }
}
