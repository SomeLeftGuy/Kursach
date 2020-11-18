using Kursach.Filters;
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
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _db;
        public HomeController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public async Task<IActionResult> Index(string filter = null)
        {
            if (_roleManager.Roles.Count() == 0)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Dark"));
            }
            HomeView[] refs;
            if (filter == null)
            {
                refs = _db.Fanfics.Where(item =>item.endDate > DateTime.Now).Select(item => new HomeView
                {
                    
                    Ref = item.id,
                    EndDate = Convert.ToDateTime(item.endDate),
                    endedCh = item.endedCh,
                    requiredCh = item.requiredCh,
                    Image = item.image,
                    Name = item.name
                }).ToArray();
            }
            else
            {
                filter = filter.Replace("  ", " ");
                string[] tagsString = filter.Split(" ");
                for (int i = 0; i < tagsString.Length - 1; i++)
                {
                    for (int j = i + 1; j < tagsString.Length; j++)
                    {
                        if (tagsString[i] == tagsString[j])
                        {
                            tagsString[j] = null;
                        }
                    }
                }
                Tag[] tags = new Tag[tagsString.Length];
                List<HomeView> fanfics = new List<HomeView>();
                for (int i = 0; i < tags.Length; i++)
                {
                    tags[i] = _db.Tag.FirstOrDefault(item => item.name == tagsString[i]);
                    if (tags[i] != null)
                    {
                        fanfics.AddRange(_db.TagsToFanfics.Where(item => item.tag == tags[i] && item.Fanfic.endDate > DateTime.Now).Select(item => new HomeView
                        {
                            Ref = item.Fanfic.id,
                            EndDate = Convert.ToDateTime(item.Fanfic.endDate),
                            endedCh = item.Fanfic.endedCh,
                            requiredCh = item.Fanfic.requiredCh,
                            Image = item.Fanfic.image,
                            Name = item.Fanfic.name,
                            Rating = 0
                        }));
                    }
                }
                List<HomeView> result = new List<HomeView>();
                for (int i = 0; i < fanfics.Count; i++)
                {
                    bool duplicate = false;
                    for (int z = 0; z < i; z++)
                    {
                        if (fanfics[z].Ref == fanfics[i].Ref)
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if (!duplicate)
                    {
                        result.Add(fanfics[i]);
                    }
                }
                refs = result.ToArray();
            }
            for (int i = 0; i < refs.Length; i++)
            {
                int[] marks = _db.Marks.Where(item => item.companyId == refs[i].Ref).Select(item => item.value).ToArray();
                if (marks != null)
                {
                    double rating = 0;
                    double fullRaiting = marks.Length;
                    for (int j = 0; j < marks.Length; j++)
                    {
                        rating += marks[j] + 1;
                    }
                    rating /= marks.Length;
                    fullRaiting *= rating;
                    refs[i].Rating = Math.Round(rating, 2);
                    refs[i].FullRating = fullRaiting;
                }
                else
                {
                    refs[i].FullRating = 0;
                    refs[i].Rating = 0;
                }
            }
            for (int i = 0; i < refs.Length - 1; i++)
            {
                for (int j = i + 1; j < refs.Length; j++)
                {
                    if (refs[i].FullRating < refs[j].FullRating)
                    {
                        HomeView buf = refs[i];
                        refs[i] = refs[j];
                        refs[j] = buf;
                    }
                }
            }
            return View(new HomeViewModel { Fanfics = refs });
        }
    }
}
