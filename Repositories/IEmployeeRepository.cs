using backend_dotnet.Models;

public interface IEmployeeRepository {
  Task<List<Employee>> GetAllAsync();

  Task<Employee?> GetByIdAsync(short EmployeeId);
  Task<Employee?> GetByNameAndLastNameAsync(string Name, string LastName);
  Task<Employee> Create(Employee newEmployee);

  Task<Employee?> GetWithMaxId();

  Task<Employee> EditAsync(Employee Employee);

  Task<Employee?> DeleteAsync(short Id);
}