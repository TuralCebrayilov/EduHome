using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Services : Controller
    {
        private readonly AppDbContext _Db;
        public Services(AppDbContext Db)
        {
            _Db = Db;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services= await _Db.Services.ToListAsync();
           
            return View(services);
        }
       public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Service service) 
        {
            bool isExist=await _Db.Services.AnyAsync(x=> x.Name==service.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This service is already exist !");
                return View(service);
            }
            await _Db.Services.AddAsync(service);
            await _Db.SaveChangesAsync();
           
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service _Dbservice = await _Db.Services.FirstOrDefaultAsync(x=>x.Id==id);
            if (_Dbservice == null)
            {
                return BadRequest();
            }
            return View(_Dbservice);
         
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Service service)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service _Dbservice = await _Db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (_Dbservice == null)
            {
                return BadRequest();
            }
            bool isExist = await _Db.Services.AnyAsync(x => x.Name == service.Name && x.Id!=id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This service is already exist !");
                return View(service);
            }

            _Dbservice.Name=service.Name;
            _Dbservice.Description=service.Description;
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //public async Task<IActionResult> Detail(int id)
        //{
        //    Service service = await _Db.Services.FirstOrDefaultAsync(x => x.Id == id);
        //    return View(service);
        //}
        //public async Task<IActionResult> Detail(int? id)
        //{
          
        //    Service _Dbservice = await _Db.Services.FirstOrDefaultAsync(x => x.Id == id);
           
        //    return View(_Dbservice);

        //}
        //[HttpPost]
        public async Task<IActionResult> Detail(int? id)
        {
           
            Service service = await _Db.Services.FindAsync(id);
          

            //_Dbservice.Name = service.Name;
            //_Dbservice.Description = service.Description;
          
            return View(service);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service _Dbservice = await _Db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (_Dbservice == null)
            {
                return BadRequest();
            }
            return View(_Dbservice);

        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service _Dbservice = await _Db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (_Dbservice == null)
            {
                return BadRequest();
            }
            _Dbservice.IsDeactive = true;
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service _Dbservice = await _Db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (_Dbservice == null)
            {
                return BadRequest();
            }
            if (_Dbservice.IsDeactive)
            {
                _Dbservice.IsDeactive= false;
            }
            else
            {
                _Dbservice.IsDeactive= true;
            }
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
    }
}
