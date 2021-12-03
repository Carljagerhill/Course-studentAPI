using AutoMapper;
using Cms.Data.Repository.Models;
using CmsWebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsWebApi.Mapper
{
    public class CmsMapper : Profile
    {
        public CmsMapper()
        {
            CreateMap<CourseDto, Course>()
                .ReverseMap();

            //CreateMap<Course, CourseDto>()


            CreateMap<StudentDto, Student>()
                .ReverseMap();
        }
    }
}
