using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseApp.Models;

/**
 * Created by
 * Hreidar Olafur Arnarsson, hreidara14@ru.is
 * and
 * Maciej Sierzputowski, maciej15@ru.is
 * 27 Aug 2017
 * */
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
        [Route("api/courses/{id:int}", Name = "GetCourseByID")]
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

        // POST api/courses
        [HttpPost]
        [Route("api/courses")]
        public IActionResult AddCourse([FromBody]Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(412);
            }

            _courses.Add(course);

            return CreatedAtRoute("GetCourseByID", new { id = course.ID }, course);
        }

        // PUT api/courses/5
        [HttpPut]
        [Route("api/courses/{id:int}")]
        public IActionResult UpdateCourse(int id, [FromBody]Course course)
        {
            if (course == null)
            {
                return NotFound("Could not find course with that ID!");
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(412);
            }

            _courses.ElementAt(id - 1).ID = course.ID;
            _courses.ElementAt(id - 1).Name = course.Name;
            _courses.ElementAt(id - 1).TemplateID = course.TemplateID;
            _courses.ElementAt(id - 1).StartDate = course.StartDate;
            _courses.ElementAt(id - 1).EndDate = course.EndDate;

            return Ok(course);
        }

        // DELETE api/courses/5
        [HttpDelete]
        [Route("api/courses/{id:int}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _courses.SingleOrDefault(x => x.ID == id);

            if (course == null)
            {
                return NotFound("Could not find a course with that ID!");
            }

            _courses.Remove(course);

            return StatusCode(204);
        }

        // GET api/courses/1/students
        [HttpGet]
        [Route("api/courses/{courseid:int}/students", Name = "GetStudentList")]
        public IActionResult GetStudentList(int courseid)
        {
            // if the request is sent and there are no courses
            if (_courses == null)
            {
                return BadRequest("No courses!");
            }

            var course = _courses.SingleOrDefault(x => x.ID == courseid);

            if (course == null || course.ID != courseid)
            {
                return BadRequest();
            }

            // if the list of students is empty
            if (!course.studentlist.Any())
            {
                return NotFound("No students");
            }

            return Ok(course.studentlist);
        }

        // POST api/courses/1/students
        [HttpPost]
        [Route("api/courses/{id:int}/students")]
        public IActionResult AddStudent(int id, [FromBody]Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            if(id > _courses.Count || id < 0)
            {
                return NotFound("Could not find a course with that ID!");
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(412);
            }

            var course = _courses.ElementAt(id - 1);
            course.studentlist.Add(student);

            return CreatedAtRoute("GetStudentList", new { courseid = course.ID }, course);
        }
    }
}
