﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models
{
    public class Patient
    {
        [Key]
        public int Patient_ID { get; set; }
        //primary key
        public string PatientName { get; set; }
        //patient name
        public DateTime PatientAdmittanceDate { get; set; }
        //patient admittance date
        public DateTime PatientDateOfBirth { get; set; }
        //patient DOB
    }

    public class PatientDto
    {
        public int Patient_ID { get; set; }
        //primary key
        public string PatientName { get; set; }
        //patient name
        public DateTime PatientAdmittanceDate { get; set; }
        //patient admittance date
        public DateTime PatientDateOfBirth { get; set; }
        //patient DOB
    }
}