using Micro_social_platform.Data;
using Micro_social_platform.Models;
using Microsoft.AspNetCore.Mvc;

namespace Micro_social_platform.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;
        public CommentsController(ApplicationDbContext context)
        {
            db = context;
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Comment comm = db.Comments.Find(id);
            db.Comments.Remove(comm);
            db.SaveChanges();
            TempData["message"] = "Comment deleted sucesfully";
            return Redirect("/Articles/Show/" + comm.ArticleId);
        }
        public IActionResult Edit(int id)
        {
            Comment comm = db.Comments.Find(id);
            return View(comm);
        }
        [HttpPost]
        public IActionResult Edit(int id, Comment requestComment)
        {
            requestComment.CommentId = id;
            Comment comment = db.Comments.Where(com => com.CommentId == id)
                                            .First();
            if (ModelState.IsValid)
            {
                comment.Content = requestComment.Content;
                comment.Date = requestComment.Date;
                db.SaveChanges();
                return Redirect("/Articles/Show/" + comment.ArticleId);
            }
            else
            {
                return View(requestComment);
            }
        }
    }
}