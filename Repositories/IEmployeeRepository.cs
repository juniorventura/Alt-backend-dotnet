using backend_dotnet.Models;

public interface IEmployeeRepository {
  Task<List<Employee>> GetAllAsync();
}