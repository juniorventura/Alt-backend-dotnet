using backend_dotnet.Models;

public interface IEmployeeService {
  Task<List<Employee>> GetAllAsync();
}
