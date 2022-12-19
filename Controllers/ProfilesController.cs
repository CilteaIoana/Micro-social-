/*using Micro_social_platform.Data;
using Micro_social_platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;

namespace Micro_social_platform.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ProfilesController(
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
            var profiles = db.Profiles.Include("User");
            ViewBag.Profiles = profiles;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Msg = TempData["message"].ToString();
            }
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult New()
        {
            Profile profile = new Profile();
            return View(profile);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
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

        [Authorize(Roles = "User,Admin")]
        public IActionResult Show(int id)
        {
            Profile profile = db.Profiles.Include("User")
                                         .Where(prof => prof.ProfileId == id)
                                         .First();
            ViewBag.Profile = profile;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Msg = TempData["message"].ToString();
            }
            
            return View(profile);
        }

        [Authorize(Roles = "User,Admin")]
        public ActionResult Delete(int id)
        {
            Profile profile = db.Profiles.Find(id);

            if (profile.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Profiles.Remove(profile);
                db.SaveChanges();
                TempData["message"] = "Profile deleted sucesfully";
                return Redirect("/Profiles/Show/" + profile.ProfileId);
                //return RedirectToAction("Index", "Profiles");
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti profilul";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id)
        {

            Profile profile = db.Profiles.Where(prof => prof.ProfileId == id)
                                         .First();
            ViewBag.Profile = profile;
            if (profile.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(profile);
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa editati profilul";
                return RedirectToAction("Index", "Profiles");
            }
            

        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id, Profile requestProfile)
        {
            requestProfile.ProfileId = id;
            Profile profile = db.Profiles.Find(id);

            if (ModelState.IsValid)
            {
                if (profile.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
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
                {
                    TempData["message"] = "Nu aveti dreptul sa editati profilul";
                    return RedirectToAction("Index", "Profiles");
                }
            }
            else
            {
                return View(requestProfile);
            }
        }
    }
}
*/
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
