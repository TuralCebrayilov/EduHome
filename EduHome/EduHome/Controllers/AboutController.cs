using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EduHome.Controllers
{
	public class AboutController : Controller
	{
		private readonly AppDbContext _Db;
        public AboutController(AppDbContext Db)
        {
            _Db = Db;
        }
        public IActionResult Index()
		{
			About abouts = _Db.Abouts.FirstOrDefault();

			return View(abouts);
		}
	}
}
