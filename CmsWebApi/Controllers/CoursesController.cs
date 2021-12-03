using Cms.Data.Repository.Repositories;
using Cms.Data.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsWebApi.DTOs;
using AutoMapper;

namespace CmsWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICmsRepository _cmsRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICmsRepository cmsRepository, IMapper mapper)
        {
            _cmsRepository = cmsRepository;
            _mapper = mapper;
        }



       




        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesAsync()
        {
            try
            {
                IEnumerable<Course> courses = await _cmsRepository.GetAllCoursesAsync();
                //var result = MapCourseToCourseDto(courses);
                var result = _mapper.Map<CourseDto[]>(courses);
                return result.ToList(); //Convert to support ActionResult<T>
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<CourseDto> AddCourse([FromBody] CourseDto course)
        {
            try
            {
                //if(!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                
                
                var newCourse = _mapper.Map<Course>(course);
                newCourse = _cmsRepository.AddCourse(newCourse);
                return _mapper.Map<CourseDto>(newCourse);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDto> GetCourse(int courseId)
        {
            try
            {
                if (!_cmsRepository.isCourseExists(courseId))
                    return NotFound();

                Course course = _cmsRepository.GetCourse(courseId);
                var result = _mapper.Map<CourseDto>(course);

                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{courseId}")]
        public  ActionResult<CourseDto> UpdateCourse(int courseId, CourseDto course)
        {
            try
            {
                if (!_cmsRepository.isCourseExists(courseId))
                    return NotFound();

                Course updatedCourse = _mapper.Map<Course>(course);
                updatedCourse = _cmsRepository.UpdateCourse(courseId, updatedCourse);
                var result = _mapper.Map<CourseDto>(updatedCourse);

                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{courseId}")]
        public ActionResult<CourseDto> DeleteCourse(int courseId)
        {
            try
            {
                if (!_cmsRepository.isCourseExists(courseId))
                    return NotFound();

                Course course = _cmsRepository.DeleteCourse(courseId);

                if (course == null)
                    return BadRequest();
                var result = _mapper.Map<CourseDto>(course);

                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //GET ../courses/1/students
        [HttpGet("{courseId}/students")]
        public ActionResult<IEnumerable<StudentDto>> GetStudents(int courseId)
        {
            try
            {
                if (!_cmsRepository.isCourseExists(courseId))
                    return NotFound();

              IEnumerable<Student> students = _cmsRepository.GetStudents(courseId);
                var result = _mapper.Map<StudentDto[]>(students);

                return result;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{courseId}/students")]
        public ActionResult<StudentDto> AddStudent(int courseId, StudentDto student)
        {
            try
            {
                if (!_cmsRepository.isCourseExists(courseId))
                    return NotFound();

                Student newStudent = _mapper.Map<Student>(student);

                //Assign Course
                Course course = _cmsRepository.GetCourse(courseId);
                newStudent.Course = course;

                newStudent = _cmsRepository.AddStudent(newStudent);
                var result = _mapper.Map<StudentDto>(newStudent);

                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        #region Async Functions

        //// return type - approach 1 - primary or complex type
        //[HttpGet]
        //public IEnumerable<CourseDto> GetCourses()
        //{
        //    try
        //    {
        //        IEnumerable<Course> courses = _cmsRepository.GetAllCourses();
        //        var result = MapCourseToCourseDto(courses);
        //        return result;
        //    }
        //    catch (System.Exception)
        //    {
        //        throw;
        //    }
        //}





        // return type - approach 2 - IActionResult
        //[HttpGet]
        //public IActionResult GetCourses()
        //{
        //    try

        //   {
        //        IEnumerable<Course> courses = _cmsRepository.GetAllCourses();
        //        var result = MapCourseToCourseDto(courses);
        //        return Ok(result);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}




        // return type - approach 3 - ActionResult<t>

        //[HttpGet]
        //public ActionResult<IEnumerable<CourseDto>> GetCourses()
        //{
        //    try
        //    {
        //        IEnumerable<Course> courses = _cmsRepository.GetAllCourses();
        //        var result = MapCourseToCourseDto(courses);
        //        return result.ToList(); //Convert to support ActionResult<T>
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        #endregion

        #region Custom mapper functions

        //private CourseDto MapCourseToCourseDto(Course course)
        //{
        //    return new CourseDto()
        //    {
        //        CourseId = course.CourseId,
        //        CourseName = course.CourseName,
        //        CourseDuration = course.CourseDuration,
        //        CourseType = (CmsWebApi.DTOs.COURSE_TYPE)course.CourseType
        //    };
        //}



        //private IEnumerable<CourseDto> MapCourseToCourseDto(IEnumerable<Course> courses)
        //{
        //    IEnumerable<CourseDto> result;

        //    result = courses.Select(c => new CourseDto()
        //    {
        //        CourseId = c.CourseId,
        //        CourseName = c.CourseName,
        //        CourseDuration = c.CourseDuration,
        //        CourseType = (CmsWebApi.DTOs.COURSE_TYPE)c.CourseType
        //    });

        //    return result;

        //}
        #endregion Custom Mapper Functions
    }
}
