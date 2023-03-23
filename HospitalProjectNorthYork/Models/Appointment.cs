using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace HospitalProjectNorthYork.Models
{
    public class Appointment
    {
        [Key]
        public int Appointment_ID { get; set; }
        //Primary Key
        public string AppointmentDesc { get; set; } 
        //Description of the appointment
        public DateTime AppointmentDate { get; set; }
        //Date of the appointment
    }
    /* 
     * TODO: Add foreign keys for the following tables: 
     *       Patient_ID
     *       Doctor_ID
     *       Location_ID
     */

}