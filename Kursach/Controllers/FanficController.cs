﻿using Kursach.Filters;
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
    [TypeFilter(typeof(UserFilter))]
    public class FanficController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly UserManager<User> _userManager;
        public FanficController(UserManager<User> userManager, ApplicationContext db)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> AddFanfic()
        {
            User user = await _userManager.GetUserAsync(User);
            _db.Fanfics.Add(new Fanfic
            {
                user = user,
                name = "Simple name",
                createDate = DateTime.Now,
                endedCh = "0",
                requiredCh = "0"
            });
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Profile");
        }
        public async Task<IActionResult> DeleteFanfic(ProfileViewModel model)
        {
            for (int i = 0; i < model.fanfics.Length; i++)
            {
                if (model.fanfics[i].selected)
                {
                    Fanfic fanfic = _db.Fanfics.FirstOrDefault(item => item.id == model.fanfics[i].id);
                    _db.Fanfics.Remove(fanfic);
                    await _db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Profile");
        }
        [HttpGet]
        public IActionResult EditFanfic(ProfileViewModel model)
        {
            for (int i = 0; i < model.fanfics.Length; i++)
            {
                if (model.fanfics[i].selected)
                {
                    Fanfic fanfic = _db.Fanfics.FirstOrDefault(item => item.id == model.fanfics[i].id);
                    string[] tags = _db.TagsToFanfics.Where(item => item.Fanfic == fanfic).Select(item => item.tag.name).ToArray();
                    string tag = "";
                    foreach (string buf in tags)
                    {
                        tag += buf + " ";
                    }
                    return View(new FanficViewModel
                    {
                        id = fanfic.id,
                        Text = fanfic.text,
                        FanficName = fanfic.name,
                        EndDate = fanfic.endDate,
                        requiredCh = fanfic.requiredCh,
                        Tags = tag,
                        Image = fanfic.image
                    });
                }
            }
            return RedirectToAction("Index", "Profile");
        }
        [HttpPost]
        public async Task<IActionResult> EditFanfic(FanficViewModel model)
        {
            Fanfic fanfic = _db.Fanfics.FirstOrDefault(item => item.id == model.id);
            if (_db.TagsToFanfics.FirstOrDefault(item => item.Fanfic == fanfic) != null)
                _db.TagsToFanfics.RemoveRange(_db.TagsToFanfics.Where(item => item.Fanfic == fanfic));
            await _db.SaveChangesAsync();
            fanfic.name = model.FanficName;
            fanfic.text = model.Text;
            fanfic.requiredCh = model.requiredCh;
            fanfic.endDate = model.EndDate;
            fanfic.image = model.Image;
            if (model.Tags != null)
            {
                model.Tags = model.Tags.Replace("  ", " ");
                string[] tags = model.Tags.Split(" ");
                _db.Fanfics.Update(fanfic);
                for (int i = 0; i < tags.Length; i++)
                {
                    if (tags[i] != "")
                    {
                        Tag tag = _db.Tag.FirstOrDefault(item => item.name == tags[i]);
                        if (tag == null)
                        {
                            _db.Tag.Add(new Tag { name = tags[i] });
                            await _db.SaveChangesAsync();
                            _db.TagsToFanfics.Add(new TagsToFanfics { Fanfic = fanfic, tag = _db.Tag.FirstOrDefault(item => item.name == tags[i]) });
                        }
                        else
                        {
                            _db.TagsToFanfics.Add(new TagsToFanfics { Fanfic = fanfic, tag = tag });
                        }
                    }
                }
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Profile");
        }
        [Route("Fanfic/{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            Fanfic fanfic = _db.Fanfics.FirstOrDefault(item => item.id == id);
            if (fanfic != null)
            {
                User user = await _userManager.GetUserAsync(User);
                string userName = _userManager.Users.FirstOrDefault(item => item.Id == fanfic.userId).UserName;
                ChaptersView[] chapters = _db.Chapters.Where(item => item.fanfic == fanfic)
                   .Select(item => new ChaptersView { Name = item.name, Text = item.text, Image = item.image, id = item.id }).ToArray();
                CommentsView[] comments = _db.Comments.Where(item => item.fanfic == fanfic)
                    .Select(item => new CommentsView { id = item.id, Author = item.user.UserName, Text = item.text}).ToArray();

                return View(new ShowFanficViewModel
                {
                    Id = fanfic.id,
                    Name = fanfic.name,
                    Image = fanfic.image,
                    Text = fanfic.text,
                    User = userName,
                    comments = comments,
                    Chapters = chapters,
                    addChapter = new ChaptersView()
                });

            }
            else return View("NotFound");
        }
    }
}

