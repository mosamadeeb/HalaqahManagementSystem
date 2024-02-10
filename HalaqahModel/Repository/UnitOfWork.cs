using HalaqahModel.Context;
using HalaqahModel.Models;

namespace HalaqahModel.Repository;

public class UnitOfWork(HalaqahContext context) : IDisposable
{
    private GenericRepository<Halaqah>? _halaqahs;
    private GenericRepository<HalaqahRecord>? _halaqahRecords;
    private GenericRepository<Masjid>? _masjids;
    private GenericRepository<Person>? _persons;
    private GenericRepository<Record>? _records;
    private GenericRepository<StudentAttendance>? _studentAttendances;
    private GenericRepository<Segment>? _segments;
    private GenericRepository<Semester>? _semesters;
    private GenericRepository<SemesterRecord>? _semesterRecords;
    private GenericRepository<Student>? _students;
    private GenericRepository<User>? _users;
    private GenericRepository<UserAttendance>? _userAttendances;
    
    public GenericRepository<Halaqah> Halaqahs { get { return this._halaqahs ??= new GenericRepository<Halaqah>(context); } }
    public GenericRepository<HalaqahRecord> HalaqahRecords { get { return this._halaqahRecords ??= new GenericRepository<HalaqahRecord>(context); } }
    public GenericRepository<Masjid> Masjids { get { return this._masjids ??= new GenericRepository<Masjid>(context); } }
    public GenericRepository<Person> Persons { get { return this._persons ??= new GenericRepository<Person>(context); } }
    public GenericRepository<Record> Records { get { return this._records ??= new GenericRepository<Record>(context); } }
    public GenericRepository<StudentAttendance> StudentAttendances { get { return this._studentAttendances ??= new GenericRepository<StudentAttendance>(context); } }
    public GenericRepository<Segment> Segments { get { return this._segments ??= new GenericRepository<Segment>(context); } }
    public GenericRepository<Semester> Semesters { get { return this._semesters ??= new GenericRepository<Semester>(context); } }
    public GenericRepository<SemesterRecord> SemesterRecords { get { return this._semesterRecords ??= new GenericRepository<SemesterRecord>(context); } }
    public GenericRepository<Student> Students { get { return this._students ??= new GenericRepository<Student>(context); } }
    public GenericRepository<User> Users { get { return this._users ??= new GenericRepository<User>(context); } }
    public GenericRepository<UserAttendance> UserAttendances { get { return this._userAttendances ??= new GenericRepository<UserAttendance>(context); } }
    
    public void Save()
    {
        context.SaveChanges();
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}