using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduHome.ViewComponents
{
    
	
    public class FooterViewComponent: ViewComponent
    {
        private readonly AppDbContext _Db;
        public FooterViewComponent(AppDbContext Db)
        {
            _Db = Db;
            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Footer footer = await _Db.Footers.FirstOrDefaultAsync();
            return View(footer);
        }

    }
}
