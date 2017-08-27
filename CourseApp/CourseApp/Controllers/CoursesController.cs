using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseApp.Models;

namespace CourseApp.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private static List<Course> _courses;

        public CoursesController()
        {
            if (_courses == null)
            {
                _courses = new List<Course>
                {
                    new Course
                    {
                        ID          = 1,
                        Name        = "Web services",
                        TemplateID  = "T-514-VEFT",
                        StartDate   = DateTime.Now,
                        EndDate     = DateTime.Now.AddMonths(3)
                    }
                };
            }
        }
        // GET api/courses
        [HttpGet]
        [Route("api/courses")]
        public IActionResult GetCourses()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
