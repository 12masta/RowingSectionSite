using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Identity1.Models;
﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace Identity1.Controllers
{
    public class PlayerController : Controller
    {
        private ApplicationDbContext db;

        private UserManager<ApplicationUser> manager;

        public PlayerController()
        {
            db = new ApplicationDbContext();

            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        

        // GET: /Player/
        public ActionResult Index()
        {          
            var currentUser = manager.FindById(User.Identity.GetUserId());

            return View(db.Players.ToList().Where(
                player => player.User.Id == currentUser.Id));
        }

        public ActionResult IndexPlayerException()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            return View(db.Players.ToList().Where(
                player => player.User.Id == currentUser.Id));
        }

        [Authorize(Roles="Admin")]
        public ActionResult All(string searchString, string currentFilter, int? page)
        {
            var players = from s in db.Players
                          select s;

            players = players.OrderBy(s => s.User.LastName);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if(!String.IsNullOrEmpty(searchString))
            {
                players = players.Where(s=> s.User.LastName.Contains(searchString) ||
                    s.User.FirstName.Contains(searchString));
            }
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(players.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: /Player/Create
        public ActionResult Create()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

                if (currentUser.Player == null)
                {
                    return View();
                }
                
                //ModelState.AddModelError("", "Dupa");
                return RedirectToAction("IndexPlayerException");




        
        }

        // POST: /Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,BirthDate,JoinDate")] Player player)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            try
            {

            
                if (ModelState.IsValid)
                {
                    player.User = currentUser;
                    db.Players.Add(player);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Nie można zapisać zmian, najprawdopodobniej dodałeś już zawodnika. Jeżeli jesteś pewny że problem wystąpił z innego powodu, spróbuj ponownie lub skontaktuj się z administratorem");

            }
            return View(player);
        }

        // GET: /Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: /Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,BirthDate,JoinDate")] Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(player).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Nie można zapisać zmian. Spróbuj ponownie lub skontaktuj się z administratorem.");

            }
            return View(player);
        }

        // GET: /Player/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Nie można zapisać zmian. Spróbuj ponownie lub skontaktuj się z administratorem.";
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: /Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Player player = db.Players.Find(id);
                db.Players.Remove(player);
                db.SaveChanges();
            }
            catch(DataException/* dex */)
            {
                ModelState.AddModelError("", "Nie można zapisać zmian. Spróbuj ponownie lub skontaktuj się z administratorem.");
            }
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
