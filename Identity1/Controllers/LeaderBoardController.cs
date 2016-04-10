using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Identity1.Models;
using Identity1.ViewModel;
using PagedList;

namespace Identity1.Controllers
{
    public class LeaderBoardController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationDbContext db;
        public LeaderBoardController()
        {
            db = new ApplicationDbContext();
        }
        //
        // GET: /LeaderBoard/
        public ActionResult Index()
        {            
            var meters = from s in db.Enrollments
                              select s.NumbersOfMeters;

            var trainingTime = from s in db.Enrollments
                            select s.TrainingTime;

            var players = from s in db.Players
                          select s;

            int intTotalMetres = 0;
            TimeSpan tsTotalTime = new TimeSpan(0,0,0,0);


            foreach (var total in meters)
            {
                intTotalMetres = intTotalMetres + total;
            }
            ViewBag.TotalMetres = intTotalMetres;

            foreach(var time in trainingTime)
            {
                tsTotalTime = tsTotalTime.Add(time);
            }

            ViewBag.TotalTime = tsTotalTime;
            ViewBag.TotalDays = tsTotalTime.Days;
            ViewBag.TotalHours = tsTotalTime.Hours;
            ViewBag.TotalMinutes = tsTotalTime.Minutes;

            var LeaderBoard = new List<LeadarboardPlayer>();
            foreach(var p in players)
            {
                var playerEnroll = p.Enrollments.ToList();

                int i = 0, playerNumberMeters = 0;
                double playerRate = 0;
                TimeSpan playerTotalTime = new TimeSpan(0, 0, 0, 0);
                string fullName = p.User.FullName;

                foreach(var e in playerEnroll)
                {
                    i++;
                    playerNumberMeters = playerNumberMeters + e.NumbersOfMeters;
                    playerTotalTime = playerTotalTime.Add(e.TrainingTime);
                    playerRate = playerRate + e.Rate;
                }
                playerRate = playerRate / i;
                LeaderBoard.Add(new LeadarboardPlayer(fullName, playerTotalTime, playerNumberMeters, playerRate));

            }

            ViewBag.LeaderBoardMeters = LeaderBoard.OrderByDescending(o => o.Meters);
            ViewBag.LeaderBoardTime = LeaderBoard.OrderByDescending(s => s.Time);
            ViewBag.LeaderBoardRate = LeaderBoard.OrderByDescending(s => s.Rate);

            return View();
        }
	}

    public class LeadarboardPlayer
    {
        public string FullName { get; set; }
        public TimeSpan Time { get; set; }
        public int Meters { get; set; }
        public double Rate { get; set; }

        public LeadarboardPlayer(string f, TimeSpan t, int m, double r)
        {
            FullName = f;
            Time = t;
            Meters = m;
            Rate = r;
        }
    }


}