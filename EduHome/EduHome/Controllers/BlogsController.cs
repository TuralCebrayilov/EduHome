using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EduHome.Controllers
{
	public class BlogsController : Controller
	{
		private readonly AppDbContext _Db;
        public BlogsController(AppDbContext Db)
        {
			_Db = Db;
            
        }
        public IActionResult Index()
		{
			List<Blog> blogs =_Db.Blogs.ToList();
			return View(blogs);
		}
	}
}
