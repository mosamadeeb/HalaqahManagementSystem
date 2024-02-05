using HalaqahAPI.Models;
using HalaqahAPI.Repository;

namespace HalaqahAPI.Services;

public class StudentService(IRepository<Student> students, IRepository<StudentAttendance> attendance)
{
    public void MarkAttendance(int studentId, DateTime date, AttendanceStatus status, bool hasCompleted, bool hasDress)
    {
        var student = students.GetById(studentId);
        if (student == null)
        {
            throw new KeyNotFoundException("Student not found");
        }

        if (attendance.GetAll(a => a.StudentId == studentId && a.Timestamp.Date == date.Date).Any())
        {
            throw new Exception("Attendance already marked");
        }

        var attendanceRecord = new StudentAttendance
        {
            StudentId = studentId,
            Timestamp = date,
            Status = status,
            HasCompleted = hasCompleted,
            HasDress = hasDress
        };

        attendance.Insert(attendanceRecord);
        attendance.Save();
    }
}