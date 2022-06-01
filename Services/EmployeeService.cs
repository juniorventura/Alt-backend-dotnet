using backend_dotnet.Models;
public class EmployeeService : IEmployeeService {

  private readonly IEmployeeRepository _employeeRepository;

  public EmployeeService(IEmployeeRepository employeeRepository)
  {
    _employeeRepository = employeeRepository;
  }

  public async Task<List<Employee>> GetAllAsync() {
    // some business logic
    return await _employeeRepository.GetAllAsync();
  }
}