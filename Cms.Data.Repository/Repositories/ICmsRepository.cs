using Cms.Data.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Data.Repository.Repositories
{
    public interface ICmsRepository
    {
        IEnumerable<Course> GetAllCourses();

      Task<IEnumerable<Course>> GetAllCoursesAsync();

        Course AddCourse(Course newCourse);

        bool isCourseExists(int courseId);

        Course GetCourse(int courseId);

        Course UpdateCourse(int courseId, Course newCourse);

        Course DeleteCourse(int courseId);

        // Association

        IEnumerable<Student> GetStudents(int courseId);

        Student AddStudent(Student student);

    }
}
