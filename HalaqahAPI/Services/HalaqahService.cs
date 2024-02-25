using HalaqahModel.Models;
using HalaqahModel.Repository;
using Microsoft.EntityFrameworkCore;

namespace HalaqahAPI.Services;

public class HalaqahService(UnitOfWork db)
{
    public IEnumerable<Student> GetStudentsInHalaqah(int halaqahId)
    {
        return db.Halaqahs.GetByIdThenInclude(halaqahId, nameof(Halaqah.Students))?.Students ?? [];
    }
    
    public IEnumerable<Halaqah> GetHalaqahsForAdmin(int adminId)
    {
        return db.Halaqahs.GetAll().Where(x => x.AdminId == adminId).Include("Teacher").Include("Students").ToList();
    }
}