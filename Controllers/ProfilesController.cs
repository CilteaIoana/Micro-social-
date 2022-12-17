using Micro_social_platform.Data;
using Micro_social_platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;

namespace Micro_social_platform.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext db;
        public ProfilesController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var profiles = db.Profiles;
            ViewBag.Profiles = profiles;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Msg = TempData["message"].ToString();
            }
            return View();
        }
        public IActionResult New()
        {
            Profile profile = new Profile();
            return View(profile);
        }

        [HttpPost]
        public IActionResult New(Profile profile)
        {

            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                TempData["message"] = "Profile created";
                return RedirectToAction("Index");
            }
            else
                return View(profile);
        }

        public IActionResult Show(int id)
        {
            Profile profile = db.Profiles.Where(prof => prof.ProfileId == id)
                                         .First();
            ViewBag.Profile = profile;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Msg = TempData["message"].ToString();
            }
            return View(profile);
        }

        public ActionResult Delete(int id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();
            TempData["message"] = "Profile deleted sucesfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {

            Profile profile = db.Profiles.Where(prof => prof.ProfileId == id)
                                         .First();
            ViewBag.Profile = profile;
            return View(profile);

        }

        [HttpPost]
        public IActionResult Edit(int id, Profile requestProfile)
        {
            requestProfile.ProfileId = id;
            Profile profile = db.Profiles.Find(id);

            if (ModelState.IsValid)

            {
                profile.FirstName = requestProfile.FirstName;
                profile.LastName = requestProfile.LastName;
                profile.UserName = requestProfile.UserName;
                profile.ProfilePicture = requestProfile.ProfilePicture;
                profile.Description = requestProfile.Description;
                profile.IsPrivate = requestProfile.IsPrivate;
                profile.JoinDate = requestProfile.JoinDate;
                db.SaveChanges();
                TempData["message"] = "Profile edited sucesfully";
                return RedirectToAction("Index");
            }
            else

                return View(requestProfile);
        }
    }
}
