using Micro_social_platform.Data;
using Micro_social_platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Micro_social_platform.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public CommentsController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Delete(int id)
        {
            Comment comm = db.Comments.Find(id);

            if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Comments.Remove(comm);
                db.SaveChanges();
                TempData["message"] = "Comment deleted sucesfully";
                return Redirect("/Articles/Show/" + comm.ArticleId);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti comentariul";
                return RedirectToAction("Index", "Articles");
            }
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id)
        {
            Comment comm = db.Comments.Where(com => com.CommentId == id)
                                      .First();//db.Comments.Find(id);
            if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(comm);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa editati comentariul";
                return RedirectToAction("Index", "Articles");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id, Comment requestComment)
        {
            //requestComment.CommentId = id;
            Comment comment = db.Comments.Find(id);//.Where(com => com.CommentId == id)
                                            //.First();
            if (ModelState.IsValid)
            {
                if (comment.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
                {
                    comment.Content = requestComment.Content;
                    comment.Date = requestComment.Date;

                    db.SaveChanges();
                    return Redirect("/Articles/Show/" + comment.ArticleId);
                }
                else
                {
                    return View(requestComment);
                    TempData["message"] = "Nu aveti dreptul sa editati comentariul";
                    //return RedirectToAction("Index", "Articles");
                    return Redirect("/Articles/Show/" + comment.ArticleId);
                }

            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa editati comentariul";
                //return RedirectToAction("Index", "Articles");
                return Redirect("/Articles/Show/" + comment.ArticleId);
            }
        }
    }
}