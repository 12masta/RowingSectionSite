using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Identity1.Models
{
    public class Contest
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date), Display(Name = "Data zawodów"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ContestDate { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public virtual ICollection<ContestEnrollment> ContestEnrollments { get; set; }
    }
}