using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity1.Models
{
    public class Enrollment
    {
        public int ID { get; set; }
        public int PlayerID { get; set; }
        public int TrainingID { get; set; }
        [Required]
        [Display(Name = "Czas treningu")]
        public TimeSpan TrainingTime { get; set; }
        [Required]
        [Display(Name = "Odległość [m]")]
        public int NumbersOfMeters { get; set; }
        [Display(Name = "Tempo")]
        public int Rate { get; set; }
        [Required]
        [Display(Name = "Data dodania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollemntDate { get; set; }

        public virtual Player Player { get; set; }
        public virtual Training Training { get; set; }
    }
}