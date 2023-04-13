using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduHome.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly AppDbContext _Db;
        public HeaderViewComponent(AppDbContext Db)
        {
            _Db = Db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Header header =await _Db.Headers.FirstOrDefaultAsync();
           
            return View(header);
        }
    }
}
