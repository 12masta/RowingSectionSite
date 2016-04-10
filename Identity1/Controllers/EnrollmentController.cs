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
    public class EnrollmentController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;


        public EnrollmentController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public ActionResult All(string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var enrollments = from s in db.Enrollments
                              select s;

            enrollments = enrollments.OrderBy(s => s.EnrollemntDate);

            if(!String.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(s => s.Training.Name.Contains(searchString));
            }


            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(enrollments.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Enrollment/
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var enrollments = from s in db.Enrollments
                              select s;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            
            enrollments = enrollments.OrderBy(s => s.EnrollemntDate).Where(
                        enrollment => enrollment.Player.User.Id == currentUser.Id);

            if (!String.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(s => s.Training.Name.Contains(searchString));
            }

            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(enrollments.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: /Enrollment/Create
        public ActionResult Create()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            ViewBag.PlayerID = currentUser.Player.ID;

            //ViewBag.PlayerID = new SelectList(db.Players, "ID", "ID");
            ViewBag.TrainingID = new SelectList(db.Trainings, "ID", "Name");
            
            return View();
        }

        // POST: /Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,PlayerID,TrainingID,TrainingTime,NumbersOfMeters,Rate,EnrollemntDate")] Enrollment enrollment)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());


            try
            {
                if (ModelState.IsValid)
                {
                    //enrollment.Player.User = currentUser;
                    enrollment.PlayerID = currentUser.Player.ID;
                    enrollment.EnrollemntDate = DateTime.Now;
                    db.Enrollments.Add(enrollment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Nie można zapisać zmian, najprawdopodobniej dodałeś zły format czasu treningu. Jeżeli jesteś pewny że problem wystąpił z innego powodu, spróbuj ponownie lub skontaktuj się z administratorem");

            }
            ViewBag.PlayerID = new SelectList(db.Players, "ID", "ID", enrollment.PlayerID);
            ViewBag.TrainingID = new SelectList(db.Trainings, "ID", "Name", enrollment.TrainingID);
            return View(enrollment);
        }

        // GET: /Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerID = new SelectList(db.Players, "ID", "ID", enrollment.PlayerID);
            ViewBag.TrainingID = new SelectList(db.Trainings, "ID", "Name", enrollment.TrainingID);
            return View(enrollment);
        }

        // POST: /Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,PlayerID,TrainingID,TrainingTime,NumbersOfMeters,Rate,EnrollemntDate")] Enrollment enrollment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(enrollment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Nie można zapisać zmian, najprawdopodobniej dodałeś zły format czasu treningu. Jeżeli jesteś pewny że problem wystąpił z innego powodu, spróbuj ponownie lub skontaktuj się z administratorem");

            }
            ViewBag.PlayerID = new SelectList(db.Players, "ID", "ID", enrollment.PlayerID);
            ViewBag.TrainingID = new SelectList(db.Trainings, "ID", "Name", enrollment.TrainingID);
            return View(enrollment);
        }

        // GET: /Enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: /Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Enrollment enrollment = db.Enrollments.Find(id);
                db.Enrollments.Remove(enrollment);
                db.SaveChanges();
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Nie można zapisać zmian. Spróbuj ponownie lub skontaktuj sie z administratorem.");

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
