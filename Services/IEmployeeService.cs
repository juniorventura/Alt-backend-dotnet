using backend_dotnet.Models;
using backend_dotnet.Dtos;

public interface IEmployeeService {
  Task<List<Employee>> GetAllAsync();
  Task<Employee> GetByIdAsync(short id);
   Task<Employee> CreateAsync(EmployeeDto employee);
  Task<EmployeeDto> EditAsync(EmployeeDto employeeDto);
  Task<EmployeeDto> DeleteAsync(short id);
}
