using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Identity1.Models;

namespace Identity1.ViewModel
{
    public class TrainingIndexData
    {
        public IEnumerable<Training> Trainings { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<ApplicationUser> User { get; set; }
    }
}