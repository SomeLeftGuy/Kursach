using Kursach.Models;
using Kursach.ModelViews;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly UserManager<User> _userManager;
        public ChaptersController(UserManager<User> userManager, ApplicationContext db)
        {
            _db = db;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> AddChapter(ShowFanficViewModel model)
        {
            Fanfic fanfics = _db.Fanfics.FirstOrDefault(item => item.id == model.Id);
            if (fanfics != null)
            {
                _db.Chapters.Add(new Chapters
                {
                    fanfic = fanfics,
                    name = model.addChapter.Name,
                    text = model.addChapter.Text,
                    image = model.addChapter.Image
                });
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(model.Id + "", "Fanfic");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteChapter(ShowFanficViewModel model)
        {
            _db.Chapters.Remove(_db.Chapters.FirstOrDefault(item => item.id == model.deletechapterID));
            await _db.SaveChangesAsync();
            return RedirectToAction(model.Id + "", "Fanfic");
        }
    }
}
