using Demo.DataAccess.Contexts;

namespace Demo.DataAccess.Repositories
{
    public interface IDepartmentRepository
    {
        int Add(Department dept);
        int Edit(Department dept);
        IEnumerable<Department> getAll(bool withTracking = false);
        Department? GetByID(int ID, AppDbContext dbContext);
        int Remove(Department dept);
    }
}