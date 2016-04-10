using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity1.Models
{
    public class ContestEnrollment
    {
        public int ID { get; set; }
        public int ContestID { get; set; }
        public int PlayerID { get; set; }
        [Required]
        [Display(Name = "Data dodania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        public virtual Player Player { get; set; }
        public virtual Contest Contest  { get; set; }

    }
}