using backend_dotnet.Models;

public interface IEmployeeRepository {
  Task<List<Employee>> GetAllAsync();
  Task<Employee?> GetByIdAsync(short id);
  Task<Employee> CreateAsync(Employee employee);
  Task<Employee?> GetByNameAndLastNameAsync(string name, string lastName);

  Task<Employee?> GetWithMaxIdAsync();

  Task<Employee> EditAsync(Employee employee);
  Task<Employee?> DeleteAsync(short id);
}