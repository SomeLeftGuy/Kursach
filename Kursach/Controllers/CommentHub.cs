using Kursach.Filters;
using Kursach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Kursach.Controllers
{
    [TypeFilter(typeof(UserFilter))]
    public class CommentsHub : Hub
    {
        private ApplicationContext _db;
        public CommentsHub(ApplicationContext context)
        {
            _db = context;
        }
        public async Task Send(string message, string userName, string fanfic)
        {
            User user = _db.Users.FirstOrDefault(item => item.UserName == userName);
            Comment comment = _db.Comments.FirstOrDefault(item => item.fanficId + "" == fanfic && item.user == user);
            if (comment == null)
            {
                if (message != "")
                {
                    _db.Comments.Add(new Comment { user = user, fanficId = Convert.ToInt32(fanfic), text = message });
                    _db.SaveChanges();
                }
            }
            else
            {
                if (message == "")
                {
                    _db.Comments.Remove(comment);
                    _db.SaveChanges();
                }
                else
                {
                    comment.text = message;
                    _db.Comments.Update(comment);
                    _db.SaveChanges();
                }
            }
            comment = _db.Comments.FirstOrDefault(item => item.fanficId + "" == fanfic && item.user == user);
            if (comment != null)
                await Clients.All.SendAsync("Send", message, userName, comment.id);
            else
                await Clients.All.SendAsync("Send", message, userName, 0);
        }
    }
}
