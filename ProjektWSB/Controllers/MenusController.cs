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
    public class MenusController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Menus
        public ActionResult Index()
        {
            Error.Message = "";

            int userId = Convert.ToInt32(Session["userId"]);

            var menu = db.Menus.Where(x => x.UserId == userId).ToList();
            foreach (Menu m in menu)
            {
                m.Dish = db.Dishes.Where(x => x.Id == m.DishId).FirstOrDefault();
            }

            menu.Sort((p, q) => p.Dish.Type.CompareTo(q.Dish.Type));

            return View(menu.ToList());
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Users");
            else
            {
                var Dishes = db.Dishes.Distinct();


                ViewBag.DishesList = new SelectList(Dishes, "Id", "Name");

                return View();
            }
        }

        // POST: Menus/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DishId")] Menu menu)
        {
            Error.Message = "";

            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(Session["userId"]);

                var tempMenu = db.Menus.Where(x => x.DishId == menu.DishId && x.UserId == userId).FirstOrDefault();
                if (tempMenu == null)
                {
                    menu.UserId = userId;
                    menu.RestaurantId = Convert.ToInt32(Session["userId"]);
                    db.Menus.Add(menu);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Menus");
                }
                else
                {
                    Error.Message = "Danie jest już w Menu!";
                    return RedirectToAction("Create", "Menus");
                }
            }

            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
