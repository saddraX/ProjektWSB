using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektWSB.Models;

namespace ProjektWSB.Controllers
{
    public class UsersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            Error.Message = "";
            return View();
        }

        // POST: Users/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Password,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                var tempUser = db.Users.Where(x => x.Login == user.Login).FirstOrDefault();
                if (tempUser == null)
                {
                    db.Users.Add(user);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch { }
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Create", "Users");
                }
            }

            return View(user);
        }

        public ActionResult Login()
        {
            Error.Message = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorizeLogin([Bind(Include = "Login,Password")] User user)
        {
            var userDetails = db.Users.Where(x => x.Login == user.Login && x.Password == user.Password).FirstOrDefault();

            if (userDetails == null)
            {
                Error.Message = "Nie ma takiego użytkownika!";
                return View("Login", user);
            }
            else
            {
                Session["userId"] = userDetails.Id;
                Session["userName"] = userDetails.Login;
                return RedirectToAction("Index", "Users");
            }
        }

        public ActionResult Logout()
        {
            Error.Message = "";
            Session.Clear();
            return RedirectToAction("Login", "Users");
        }
    }
}
