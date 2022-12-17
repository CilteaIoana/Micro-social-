using Micro_social_platform.Data;
using Micro_social_platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micro_social_platform.Controllers
{
    public class GroupMessagesController : Controller
    {
        private readonly ApplicationDbContext db;
        public GroupMessagesController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult New(GroupMessage post)
        {
            post.Date = DateTime.Now;

            try
            {
                db.GroupMessages.Add(post);
                db.SaveChanges();
                return Redirect("/Groups/Show/" + post.GroupId);
            }

            catch (Exception)
            {
                return Redirect("/groups/Show/" + post.GroupId);
            }

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            GroupMessage post = db.GroupMessages.Find(id);
            db.GroupMessages.Remove(post);
            db.SaveChanges();
            return Redirect("/Groups/Show/" + post.GroupId);
        }
        public IActionResult Edit(int id)
        {
            GroupMessage post = db.GroupMessages.Find(id);
            ViewBag.GroupMessage = post;
            return View(post);
        }
        [HttpPost]
        public IActionResult Edit(int id, GroupMessage requestGroupMessage)
        {
            GroupMessage post = db.GroupMessages.Find(id);
            try
            {

                post.Content = requestGroupMessage.Content;

                db.SaveChanges();

                return Redirect("/Groups/Show/" + post.GroupId);
            }
            catch (Exception e)
            {
                return Redirect("/Groups/Show/" + post.GroupId);
            }


        }
    }
}
