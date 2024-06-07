using StudentDetailsAPI.Models;

namespace StudentDetailsAPI.Repository
{
    public interface IStudent
    {
        string GetStudent();
        void AddStudent(StudentDetails student);
        void UpdateStudent(int studentId, StudentDetails student);
        void DeleteStudent(StudentDetails student);
        
        void ModifyStudent(int  studentId, StudentDetails student);
        void DeleteStudent(int studentId);
    }
}
