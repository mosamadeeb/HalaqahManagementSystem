using HalaqahModel.Models;
using HalaqahModel.Repository;

namespace HalaqahAPI.Services;

public class StudentService(UnitOfWork db)
{
    public Student? GetStudent(int studentId)
    {
        return db.Students.GetByIdThenInclude(studentId, nameof(Student.Person));
    }
    
    public IQueryable<Student> GetAllStudents()
    {
        return db.Students.GetAll();
    }
    
    public void CreateStudent(Person person, Student student)
    {
        db.Persons.Insert(person);
        db.Save();
        
        // Set person ID after inserting the person to ensure the ID is generated.
        student.PersonId = person.Id;
        
        db.Students.Insert(student);
        db.Save();
    }
    
    public void MarkAttendance(StudentAttendance attendanceRecord)
    {
        var student = db.Students.GetById(attendanceRecord.StudentId);
        if (student == null)
        {
            throw new KeyNotFoundException("Student not found");
        }

        if (db.StudentAttendances.GetAll().Any(a => a.StudentId == attendanceRecord.StudentId && a.Timestamp.Date == attendanceRecord.Timestamp.Date))
        {
            throw new Exception("Attendance already marked");
        }
        
        attendanceRecord.Student = null;

        db.StudentAttendances.Insert(attendanceRecord);
        db.Save();
    }
}