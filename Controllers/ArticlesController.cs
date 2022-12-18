using Micro_social_platform.Data;
using Micro_social_platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micro_social_platform.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ArticlesController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Index()
        {
           
            var articles = db.Articles.Include("User");
            ViewBag.Articles = articles;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Msg = TempData["message"].ToString();
            }
            return View();
        }
        public IActionResult Show (int id) 
        {
            Article article = db.Articles.Include("Comments")
                                         .Where(art => art.Id == id) 
                                         .First();  
            ViewBag.Article = article; 
            return View(article);
        }
        [HttpPost]
        public IActionResult Show([FromForm] Comment comment)
        {
            comment.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return Redirect("/Articles/Show/" + comment.ArticleId);
            }

            else
            {
                Article art = db.Articles.Include("Comments")
                               .Where(art => art.Id == comment.ArticleId)
                               .First();

                return View(art);
            }
        }
        public IActionResult New()
        {
            Article article = new Article();
            return View(article);
        }
        [HttpPost]
        public IActionResult New (Article article) 
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                TempData["message"] = "Post loaded";
                return RedirectToAction("Index");
            }
            else
            {
                return View(article);
            }
        }

        public IActionResult Edit(int id) 
        {

            Article article = db.Articles.Where(art => art.Id == id)
                                         .First();
            ViewBag.Article=article;    
            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(int id, Article requestArticle)
        {
            Article article = db.Articles.Find(id);

            if (ModelState.IsValid) 
                {
                    article.Title = requestArticle.Title;
                    article.Content = requestArticle.Content;
                    article.Date = requestArticle.Date;
                    db.SaveChanges();
                    TempData["message"] = "Post edited sucesfully";
                    return RedirectToAction("Index");
                }
            else
                    return View(requestArticle);
            
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            TempData["message"] = "Post deleted sucesfully";
            return RedirectToAction("Index");
        }


    }
}
