using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Identity1.Models
{
    public class Training
    {
        public int ID { get; set; }
        //public int TrainerID { get; set; }
        [Required]
        [Display(Name = "Nazwa treningu")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Data treningu")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TrainingDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}