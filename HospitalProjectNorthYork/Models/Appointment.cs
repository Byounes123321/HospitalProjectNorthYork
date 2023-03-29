using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("Patient")]
        public int? Patient_ID { get; set; }
        public virtual Patient Patient { get; set; }
        [ForeignKey("Doctors")]
        public int Doctor_ID { get; set; }
        public virtual Doctors Doctors { get; set; }
        [ForeignKey("Location")]
        public int Location_ID { get; set; }
        public virtual Location Location { get; set; }
    }
    /* 
     * TODO: Add foreign keys for the following tables: 
     *       Patient_ID
     *       Doctor_ID
     *       Location_ID
     */

}