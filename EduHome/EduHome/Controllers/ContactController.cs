﻿using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
