using Micro_social_platform.Data;
using Micro_social_platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micro_social_platform.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext db;
        public GroupsController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var groups = db.Groups;
            ViewBag.Groups = groups;
            return View();
        }

        public IActionResult Show(int id)
        {
            Group group = db.Groups.Include("GroupMessages")
                                         .Where(art => art.GroupId == id)
                                         .First();
            ViewBag.Group = group;
            return View(group);
        }
    }
}
