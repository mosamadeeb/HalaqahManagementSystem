using HalaqahAPI.Models;
using HalaqahAPI.Repository;

namespace HalaqahAPI.Services;

    public void MarkAttendance(int studentId, DateTime date, AttendanceStatus status, bool hasCompleted, bool hasDress)
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