using backend_dotnet;
using backend_dotnet.Models;
using Microsoft.EntityFrameworkCore;
public class EmployeeRepository : IEmployeeRepository {

  // Access to database 
  private readonly AltDbContext _context;

  // Injection of db context to repository class
  public EmployeeRepository(AltDbContext context)
  {
    _context = context;
  }

  public async Task<List<Employee>> GetAllAsync() {
    // LINQ query
    var query = from employee in _context.Employees select employee;

    return await query.ToListAsync();
  }

  public async Task<Employee?> GetByIdAsync(short id) {

    var query = from employee in _context.Employees
      where employee.EmployeeId == id select employee;

    return await query.FirstOrDefaultAsync();
  }

  public async Task<Employee> CreateAsync(Employee employee) {
    await _context.AddAsync<Employee>(employee);
    await _context.SaveChangesAsync();
    return employee;
  }

  public async Task<Employee?> GetByNameAndLastNameAsync(string name, string lastName) {
    var query = from employee in _context.Employees
      where employee.FirstName == name && employee.LastName == lastName select employee;

    return await query.FirstOrDefaultAsync();
  }

  public async Task<Employee?> GetWithMaxIdAsync() {
    var query = _context.Employees.OrderByDescending(x => x.EmployeeId);

    return await query.FirstOrDefaultAsync();
  }

  public async  Task<Employee> EditAsync(Employee employee) {
    _context.Entry<Employee>(employee).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return employee;
  }

  public async Task<Employee?> DeleteAsync(short id) {
    var toDelete = await GetByIdAsync(id);
    if (toDelete is null) {
      return toDelete;
    }
    _context.Employees.Remove(toDelete);
    await _context.SaveChangesAsync();

    return toDelete;
  }
}