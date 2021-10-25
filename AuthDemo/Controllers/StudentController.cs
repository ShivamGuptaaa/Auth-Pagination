using AuthDemo.Authentication;
using AuthDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        [HttpGet]
        [Route("AddStudent")]
        public string AddStudent(string name, string gender, int std,string div)
        {
            var lastStudent = db.Students.ToArray().LastOrDefault();
            int nextRoll;
            if (lastStudent == null)
                nextRoll = 1;
            else
                nextRoll = lastStudent.sRoll + 1;
            //var tmp = db.Students.ToArray().Last();
            db.Students.Add(new Student() { sRoll = nextRoll, sName = name, sStd = std, sDiv = div, sGender = gender });
            db.SaveChanges();
            return $"{name} added in the record";
        }


        [HttpGet]
        [Route("Add100Student")]
        public string Add100Student()
        {
            var lastStudent = db.Students.ToArray().LastOrDefault();
            int nextRoll;
            if (lastStudent == null)
                nextRoll = 1;
            else
                nextRoll = lastStudent.sRoll + 1;
            Random rdm = new Random();
            bool gender;
            for (var i = nextRoll; i < nextRoll + 100; i++)
            {
                Student st = new Student();
                st.sRoll = i;
                st.sName = "Student" + i;
                st.sStd = rdm.Next(1, 11);
                st.sDiv = Convert.ToChar(rdm.Next(65, 70)).ToString();
                gender = rdm.Next() > (Int32.MaxValue / 2);
                if (gender)
                    st.sGender = "Male";
                else
                    st.sGender = "Female";
                db.Students.Add(st);
            }
                db.SaveChanges();
            return "100 Students added";
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("getStudents")]
        public IActionResult getStudents(object page)
        {

            var pageNum = Convert.ToInt32(page.ToString());
            pageNum -=1;
            var students = db.Students.FromSqlRaw($"GetStudents {pageNum}").ToList();
            var tmp = PartialView("data", students);
            return tmp;
        }
        [HttpGet]
        [Route("getStudents")]
        public IActionResult getStudents()
        {
            var students = db.Students.FromSqlRaw($"GetStudents 0").ToList();
            int studentLen;
            List<StudentwLen> studentwLen = new List<StudentwLen>();
            var tmp = students.Any();
            if (!students.Any())
            {
                //studentLen = 0;
                return View(studentwLen);
            }
                studentLen = db.Students.Count();
                studentwLen = Map(students, studentLen);       
                return View(studentwLen);
        }

        [HttpGet]
        [Route("Mapper")]
        public List<Model.StudentwLen> Map(List<Student> sList,int sLen)
        {
            List<Model.StudentwLen> st = new List<Model.StudentwLen>();
            foreach (var s in sList)
            {
                Model.StudentwLen obj = new Model.StudentwLen();
                obj.sRoll = s.sRoll;
                obj.sName = s.sName;
                obj.sStd = s.sStd;
                obj.sDiv = s.sDiv;
                obj.sGender = s.sGender;
                obj.sRecordLen = sLen;
                st.Add(obj);
            }
            return st;
        }
    }

}
