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
    public class ContestEnrollmentController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;

        public ContestEnrollmentController()
        {
             db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public ActionResult All(string searchString, string currentFilter, int? page)
        {
            var contestEnrollments = from s in db.ContestEnrollments
                                     select s;
            contestEnrollments = contestEnrollments.OrderBy(s => s.EnrollmentDate);
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
                contestEnrollments = contestEnrollments.Where(s => s.Contest.Name.Contains(searchString));
            }

            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(contestEnrollments.ToPagedList(pageNumber, pageSize));
        }
        // GET: /ContestEnrollment/
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            //var contestenrollments = db.ContestEnrollments.Include(c => c.Player).Include(x => x.Contest).Include(z=>z.Player.User);
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var contestEnrollments = from s in db.ContestEnrollments
                                     select s;

            contestEnrollments = contestEnrollments.OrderBy(s => s.EnrollmentDate).Where(
               contestEnrollment => contestEnrollment.Player.User.Id == currentUser.Id);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                contestEnrollments = contestEnrollments.Where(s => s.Contest.Name.Contains(searchString));
            }

            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(contestEnrollments.ToPagedList(pageNumber, pageSize));
        }

        // GET: /ContestEnrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestEnrollment contestenrollment = db.ContestEnrollments.Find(id);
            if (contestenrollment == null)
            {
                return HttpNotFound();
            }
            return View(contestenrollment);
        }

        // GET: /ContestEnrollment/Create
        public ActionResult Create()
        {
            ViewBag.ContestID = new SelectList(db.Contests, "ID", "Name");
            ViewBag.PlayerID = new SelectList(db.Players, "ID", "ID");
            ViewBag.UserID = new SelectList(db.Users,"ID", "FullName");
            return View();
        }

        // POST: /ContestEnrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,ContestID,PlayerID,EnrollmentDate")] ContestEnrollment contestenrollment)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());


            if (ModelState.IsValid)
            {
                //contestenrollment.Player.User = currentUser;
                contestenrollment.PlayerID = currentUser.Player.ID;
                contestenrollment.EnrollmentDate = DateTime.Now;
                db.ContestEnrollments.Add(contestenrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayerID = new SelectList(db.Players, "ID", "ID", contestenrollment.PlayerID);
            return View(contestenrollment);
        }

        // GET: /ContestEnrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestEnrollment contestenrollment = db.ContestEnrollments.Find(id);
            if (contestenrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerID = new SelectList(db.Players, "ID", "ID", contestenrollment.PlayerID);
            ViewBag.ContestID = new SelectList(db.Contests, "ID", "Name");

            return View(contestenrollment);
        }

        // POST: /ContestEnrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,ContestID,PlayerID,EnrollmentDate")] ContestEnrollment contestenrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contestenrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerID = new SelectList(db.Players, "ID", "ID", contestenrollment.PlayerID);
            return View(contestenrollment);
        }

        // GET: /ContestEnrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestEnrollment contestenrollment = db.ContestEnrollments.Find(id);
            if (contestenrollment == null)
            {
                return HttpNotFound();
            }
            return View(contestenrollment);
        }

        // POST: /ContestEnrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContestEnrollment contestenrollment = db.ContestEnrollments.Find(id);
            db.ContestEnrollments.Remove(contestenrollment);
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
