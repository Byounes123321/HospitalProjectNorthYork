using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models
{
    public class FAQ
    {
        [Key]
        public int Faq_ID { get; set; }
        //primary key
        public string Ques { get; set; }
        // questions
        public string Answer { get; set; }
        // answer
        // An FAQ can belong to multiple departments
        public ICollection<Department> Departments { get; set; }
    }
}