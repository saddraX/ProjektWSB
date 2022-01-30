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
    public class DishesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Dishes
        public ActionResult Index()
        {
            Error.Message = "";

            return View(db.Dishes.ToList());
        }

        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // GET: Dishes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dishes/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Type,Price")] Dish dish)
        {
            Error.Message = "";

            if (ModelState.IsValid)
            {
                var tempDish = db.Dishes.Where(x => x.Name == dish.Name).FirstOrDefault();
                if (tempDish == null)
                {
                    if (dish.Price > 0)
                    {
                        dish.UserId = Convert.ToInt32((Session["userId"]));
                        db.Dishes.Add(dish);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Dishes");
                    }
                    else
                    {
                        Error.Message = "Nieodpowiednia cena!";
                        return RedirectToAction("Create", "Dishes");
                    }
                }
                else
                {
                    Error.Message = "Danie już istnieje!";
                    return RedirectToAction("Create", "Dishes");
                }
            }

            return View(dish);
        }

        // GET: Dishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Type,Price")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                if (dish.Price > 0)
                {
                    db.Entry(dish).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Error.Message = "Nieodpowiednia cena!";
                    return RedirectToAction("Edit", "Dishes", dish.Id);
                }
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
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
