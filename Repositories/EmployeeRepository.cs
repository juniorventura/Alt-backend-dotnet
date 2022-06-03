using backend_dotnet;
using backend_dotnet.Models;
using Microsoft.EntityFrameworkCore;
public class EmployeeRepository : IEmployeeRepository
{

  // Access to database 
  private readonly AltDbContext _context;

  // Injection of db context to repository class
  public EmployeeRepository(AltDbContext context)
  {
    _context = context;
  }

  public async Task<List<Employee>> GetAllAsync()
  {
    // LINQ query
    var query = from employee in _context.Employees select employee;

    return await query.ToListAsync();
  }

  public async Task<Employee?> GetByIdAsync(short employeeId)
  {
    // LINQ query
    var query = from employee in _context.Employees
                where employee.EmployeeId == employeeId
                select employee;

    return await query.FirstOrDefaultAsync();
  }

  public async Task<Employee?> GetByNameAndLastNameAsync(string Name, string LastName)
  {

    var query = from employee in _context.Employees
                where employee.FirstName == Name && employee.LastName == LastName
                select employee;

    return await query.AsNoTracking().FirstOrDefaultAsync();
  }

  public async Task<Employee> Create(Employee newEmployee)
  {
    await _context.AddAsync<Employee>(newEmployee);
    await _context.SaveChangesAsync();
    return newEmployee;
  }
  public async Task<Employee?> GetWithMaxId()
  {
    var query = _context.Employees.OrderByDescending(x => x.EmployeeId)
    .AsNoTracking().FirstOrDefaultAsync();

    return await query;
  }

  public async Task<Employee> EditAsync(Employee Employee)
  {
    _context.Entry<Employee>(Employee).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return Employee;
  }

  public async Task<Employee?> DeleteAsync(short Id)
  {
    var toDelete = await GetByIdAsync(Id);
    if (toDelete is null) {
      return toDelete;
    }
    _context.Employees.Remove(toDelete);
    await _context.SaveChangesAsync();

    return toDelete;
  }
}