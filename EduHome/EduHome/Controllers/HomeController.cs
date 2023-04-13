using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _Db;
		public HomeController(AppDbContext Db)
		{
				_Db = Db;
		}

        public async Task<IActionResult> Index()
		{
			

			HomeVM homeVM = new HomeVM
			{
				Sliders =await _Db.Sliders.Where(x => !x.IsDeactive).ToListAsync(),
				Services =await _Db.Services.Where(x => !x.IsDeactive).ToListAsync(),
				Headers = await _Db.Headers.FirstOrDefaultAsync(),
				Footers = await _Db.Footers.FirstOrDefaultAsync(),
            };
			return View(homeVM);
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
