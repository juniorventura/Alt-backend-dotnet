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
}