using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EduHome.Controllers
{
	public class CoursesController : Controller
	{
		private readonly AppDbContext _Db;
        public CoursesController(AppDbContext Db)
        {
            _Db = Db;
        }
        public IActionResult Index()
		{
			List<Course> courses = _Db.Courses.ToList();
					
			return View(courses);
		}
	}
}
