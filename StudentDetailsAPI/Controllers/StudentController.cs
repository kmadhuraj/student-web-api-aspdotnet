using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDetailsAPI.Dtos;
using StudentDetailsAPI.Models;
using StudentDetailsAPI.Repository;

namespace StudentDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _student;
        private readonly IMapper _mapper;
        public StudentController(IStudent student)
        {
            _student = student;
            //_mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetStudent(StudentDetailsDto student)
        {
            //var StudentDetail = Mapper.map
            var studentList = _student.GetStudent();
            return Ok(studentList);
        }
        [HttpPost]
        public ActionResult AddStudent(StudentDetails studentdetailsObj)
        {
            _student.AddStudent(studentdetailsObj);
            return Ok(studentdetailsObj);   
        }
        [HttpDelete("{studentId}")]
        public ActionResult DeleteStudent(int studentId)
        {
            _student.DeleteStudent(studentId);
            return Ok();
        }


        [HttpPut("{studentId}")]
        public ActionResult UpdateStudent(int studentId, StudentDetails updatedStudent)
        {
            _student.UpdateStudent(studentId, updatedStudent);
            return Ok(updatedStudent);
        }

        [HttpPatch]
        public ActionResult ModifyStudent(int StudentId, StudentDetails modifyStudent)
        {
            _student.ModifyStudent(StudentId, modifyStudent);
            return Ok();
        }
        
    }

}
