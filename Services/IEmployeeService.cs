using backend_dotnet.Models;
using backend_dotnet.Dtos;

public interface IEmployeeService {
  Task<List<Employee>> GetAllAsync();
  
  Task<EmployeeDto> GetByIdAsync(short employeeId);

  Task<Employee> CreateAsync(EmployeeDto employee);

  Task<Employee> EditAsync(EmployeeDto Employee);

  Task<Employee> DeleteAsync(short Id);
}
