using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DPFirst.DAL.Entities;
using DPFirst.UI.Models;

namespace DPFirst.UI.Controllers
{
    public class UserController : Controller
    {
        private UsersEntities db = new UsersEntities();

        // GET: User
        public ActionResult Index()
        {
            return View(db.NewUsers.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewUser newUser = db.NewUsers.Find(id);
            if (newUser == null)
            {
                return HttpNotFound();
            }
            return View(newUser);
        }

    
        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                NewUser newUser = new NewUser();
                newUser.ID = user.ID;
                newUser.Username=user.Username;
                newUser.Email = user.Email;
                newUser.Password = user.Password;   

                
                db.NewUsers.Add(newUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // If ModelState is not valid, return the view with validation errors
            ViewBag.User = true;
            return View("~/Views/Home/Index.cshtml");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewUser newUser = db.NewUsers.Find(id);
            if (newUser == null)
            {
                return HttpNotFound();
            }
            return View(newUser);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Email,Password")] NewUser newUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newUser);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewUser newUser = db.NewUsers.Find(id);
            if (newUser == null)
            {
                return HttpNotFound();
            }
            return View(newUser);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewUser newUser = db.NewUsers.Find(id);
            db.NewUsers.Remove(newUser);
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
