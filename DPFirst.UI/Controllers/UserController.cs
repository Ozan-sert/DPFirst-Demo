using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DPFirst.BLL.Services;
using DPFirst.BLL.ViewModel;

namespace DPFirst.UI.Controllers
{
    public class UserController : Controller
    {
        
        private UserService userService = new UserService();
        // GET: User
        public ActionResult Index()
        {
            return View(userService.GetAllUsers());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             // NewUser newUser = db.NewUsers.Find(id);
                UserViewModel newUser = userService.GetUserById(id);
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
               userService.CreateUser(user);
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
            // NewUser newUser = db.NewUsers.Find(id);
            var newUser = userService.GetUserById(id.Value);
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
        public ActionResult Edit([Bind(Include = "ID,Username,Email,Password")] UserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                userService.UpdateUser(newUser);  
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
            UserViewModel newUser = userService.GetUserById(id);
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
             userService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
