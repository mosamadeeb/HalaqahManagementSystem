using HalaqahModel.Models;
using HalaqahModel.Repository;

namespace HalaqahAPI.Services;

public class StudentService(
    IRepository<Person> persons,
    IRepository<Student> students,
    IRepository<StudentAttendance> attendances)
{
    public Student? GetStudent(int studentId)
    {
        return students.GetByIdThenInclude(studentId, nameof(Student.Person));
    }
    
    public IQueryable<Student> GetAllStudents()
    {
        return students.GetAll();
    }
    
    public void CreateStudent(Person person, Student student)
    {
        persons.Insert(person);
        persons.Save();
        
        // Set person ID after inserting the person to ensure the ID is generated.
        student.PersonId = person.Id;
        
        students.Insert(student);
        students.Save();
    }
    
    public void MarkAttendance(int studentId, StudentAttendance attendanceRecord)
    {
        var student = students.GetById(studentId);
        if (student == null)
        {
            throw new KeyNotFoundException("Student not found");
        }

        if (attendances.GetAll().Any(a => a.StudentId == studentId && a.Timestamp.Date == attendanceRecord.Timestamp.Date))
        {
            throw new Exception("Attendance already marked");
        }

        attendances.Insert(attendanceRecord);
        attendances.Save();
    }
}