using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Authentication
{
    public class Student
    {
        [Key]
        public int sRoll { get; set; }
        public string sName { get; set; }
        public string sGender { get; set; }
        public int sStd { get; set; }
        public string sDiv { get; set; }

    }
}
