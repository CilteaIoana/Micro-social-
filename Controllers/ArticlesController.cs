﻿using Micro_social_platform.Data;
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

        [Authorize(Roles = "User,Admin")]
        public IActionResult Show(int id)
        {
            Article article = db.Articles.Include("Comments")
                                         .Include("User")
                                         .Where(art => art.Id == id)
                                         .First();

            SetAccessRights();
            ViewBag.Article = article;
            return View(article);
        }


        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Show([FromForm] Comment comment)
        {
            comment.Date = DateTime.Now;
            comment.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return Redirect("/Articles/Show/" + comment.ArticleId);
            }

            else
            {
                Article art = db.Articles.Include("Comments")
                                         .Include("User")
                               .Where(art => art.Id == comment.ArticleId)
                               .First();
                SetAccessRights();
                return View(art);
            }
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult New()
        {
            Article article = new Article();
            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult New(Article article)
        {
            article.UserId = _userManager.GetUserId(User);
            article.Date = DateTime.Now;

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

        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id)
        {
            Article article = db.Articles.Where(art => art.Id == id)
                                         .First();

            if (article.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                ViewBag.Article = article;
                return View(article);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui articol ce nu va apartine";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id, Article requestArticle)
        {
            Article article = db.Articles.Find(id);

            if (ModelState.IsValid) 
            {
                if(article.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
                {
                    article.Title = requestArticle.Title;
                    article.Content = requestArticle.Content;
                    article.Date = requestArticle.Date;
                    db.SaveChanges();
                    TempData["message"] = "Post edited sucesfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui articol ce nu va apartine";
                    return RedirectToAction("Index");
                }
                   
            }
            else
                    return View(requestArticle);
            
        }
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Delete(int id)
        {
            Article article = db.Articles.Include("Comments")
                                            .Where(art => art.Id == id)
                                            .First();
            if (article.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Articles.Remove(article);
                db.SaveChanges();
                TempData["message"] = "Post deleted sucesfully";
                return RedirectToAction("Index");
            }
            else 
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un articol ce nu va apartine";
                return RedirectToAction("Index");
            }
        }

        private void SetAccessRights()
        {
            ViewBag.DisplayButtons = false;

            if (User.IsInRole("User"))
            {
                ViewBag.DisplayButtons = true;
            }

            ViewBag.UserCurent = _userManager.GetUserId(User);
            ViewBag.isAdmin = User.IsInRole("Admin");
        }

    }
}
