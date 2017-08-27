using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseApp.Models;

namespace CourseApp.Controllers
{
    //[Route("api/[controller]")]
    // if the above is uncommented then the Route for GetCourses needs to be empty
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
                        EndDate     = DateTime.Now.AddMonths(3),
                        studentlist = new List<Student>
                        {
                            new Student
                            {
                                SSN     = 0506905549,
                                Name    = "Rónald Dónaldsson"
                            },
                            new Student
                            {
                                SSN     = 0203930939,
                                Name    = "Dóróthea Landkönnuður"
                            }
                        }
                    },
                    new Course
                    {
                        ID          = 2,
                        Name        = "Programming 101",
                        TemplateID  = "T-101-PROG",
                        StartDate   = DateTime.Now,
                        EndDate     = DateTime.Now.AddMonths(3),
                        studentlist = new List<Student>
                        {
                            new Student
                            {
                                SSN     = 1010900099,
                                Name    = "Denni Dæmalausi"
                            },
                            new Student
                            {
                                SSN     = 0090099999,
                                Name    = "Mr Foreign Exchange Student"
                            }
                        }
                    }
                };
            }
        }

        // GET api/courses
        [HttpGet]
        [Route("api/courses")]
        public IActionResult GetCourses()
        {
            if (_courses == null)
            {
                return NotFound();
            }

            return Ok(_courses);
        }

        // GET api/courses/5
        [HttpGet]
        [Route("api/courses/{id}")]
        public IActionResult GetCourseByID(int id)
        {
            var course = _courses.SingleOrDefault(x => x.ID == id);

            // if you put in id 42 and there is no course with that ID then it should return NotFound
            if (course == null)
            {
                return NotFound("Could not find a course with that ID!");
            }

            // if for some reason the IDs suddenly don't match
            if (course.ID != id)
            {
                return BadRequest();
            }

            return Ok(course);
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
