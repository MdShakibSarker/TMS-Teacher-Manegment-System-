using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Migrations;
using TMS.Models;

namespace TMS.Controllers
{
	public class HomeController : Controller
	{
		private readonly TeacherDbContext teacherDb;

		//private readonly ILogger<HomeController> _logger;

		//public HomeController(ILogger<HomeController> logger)
		//{
		//	_logger = logger;
		//}

		public HomeController(TeacherDbContext teacherDb)
        {
			this.teacherDb = teacherDb;
		}

        public IActionResult Index()
		{
			var tchData = teacherDb.Teachers.ToList();
			return View(tchData);
		}
        public IActionResult CreateNew()
        {
            
            return View();
        }
		[HttpPost]
        public async Task<IActionResult> CreateNew(Teacher tch)
        {
			if (ModelState.IsValid)
			{
				await teacherDb.Teachers.AddAsync(tch);
				await teacherDb.SaveChangesAsync();
				return RedirectToAction("Index","Home");
			}

            return View(tch);
        }
        public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || teacherDb.Teachers == null)
			{
				return NotFound();
			}
			var tchData = await teacherDb.Teachers.FindAsync(id);
			if(tchData == null)
			{
				return NotFound();
			}
			return View(tchData);
		}
		[HttpPost]
		public async Task<IActionResult> Edit (int? id, Teacher tch)
		{
			if (id !=tch.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				teacherDb.Teachers.Update(tch);
				await teacherDb.SaveChangesAsync();
				return RedirectToAction("Index","Home");
			}
			return View(tch);
		}
		public async Task<IActionResult> Delete(int? id)
		{
            if (id == null || teacherDb.Teachers == null)
            {
                return NotFound();
            }
            var tchData = await teacherDb.Teachers.FirstOrDefaultAsync(x=>x.Id == id);
            if (tchData == null)
            {
                return NotFound();
            }
            return View(tchData);
		}
		[HttpPost,ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int? id)
		{
            var tchData = await teacherDb.Teachers.FindAsync(id);
			if (tchData != null)
			{
				teacherDb.Teachers.Remove(tchData);
			}
			await teacherDb.SaveChangesAsync();
			return RedirectToAction("Index","Home");
        }
        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
