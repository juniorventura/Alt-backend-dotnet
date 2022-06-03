using backend_dotnet.Models;
using backend_dotnet.Dtos;
using AutoMapper;
public class EmployeeService : IEmployeeService {

  private readonly IEmployeeRepository _employeeRepository;
  private readonly IMapper _mapper;

  public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
  {
    _employeeRepository = employeeRepository;
    _mapper = mapper;
  }

  public async Task<List<Employee>> GetAllAsync() {
    // some business logic
    return await _employeeRepository.GetAllAsync();
  }

  public async Task<Employee> GetByIdAsync(short id) {
    
    // Some validation examples
    if (id <= 0) {
      throw new InvalidOperationException("The id must be greater than 0.");
    }
    var employee = await _employeeRepository.GetByIdAsync(id);

    if (employee is null) {
      throw new InvalidOperationException("Employee was not found for id: " + id);
    }
    return employee;
  }

  public async Task<Employee> CreateAsync(EmployeeDto employee) {

    // business rules
    if (string.IsNullOrEmpty(employee.FirstName)
    || string.IsNullOrEmpty(employee.LastName) || string.IsNullOrEmpty(employee.Title)) {
      throw new InvalidOperationException("Required fields are missing.");
    }

    // Another business rule (user should not exist with same name and lastname)
    var existingEmployee = await _employeeRepository.GetByNameAndLastNameAsync(employee.FirstName, employee.LastName);

    if (existingEmployee is not null) {
      throw new InvalidOperationException("There is an existing user with that name and lastname.");
    }

    // Transform the Dto to an entity Model

    var employeeEntity = await _employeeRepository.GetWithMaxIdAsync();

    // This is symbolic
    short newId = 0;
    if (employeeEntity is null) {
      newId = 1;
    } else {
      newId = (short)(employeeEntity.EmployeeId + 1);
    }

    var newEmployee = _mapper.Map<Employee>(employeeEntity);

    // This is just a workaround, in reality this would be just a single line mapping
    newEmployee.EmployeeId = newId;
    newEmployee.FirstName = employee.FirstName;
    newEmployee.LastName = employee.LastName;
    newEmployee.Title = employee.Title;

    var newCreatedEmployee = await _employeeRepository.CreateAsync(newEmployee);
    return newCreatedEmployee;
  }

  public async Task<EmployeeDto> EditAsync(EmployeeDto employeeDto) {

    if (employeeDto.EmployeeId <= 0) {
      throw new InvalidOperationException("There is no employee with an id 0");
    }
    var existingEmployee = await _employeeRepository.GetByIdAsync(employeeDto.EmployeeId);

    if (existingEmployee is null) {
      throw new InvalidOperationException("Employee was not found with id: " + employeeDto.EmployeeId);
    }

    existingEmployee.LastName = employeeDto.LastName;
    existingEmployee.FirstName = employeeDto.FirstName;
    existingEmployee.Title = employeeDto.Title;

    var modifiedEmployee = await _employeeRepository.EditAsync(existingEmployee);

    return _mapper.Map<EmployeeDto>(modifiedEmployee);
  }

  public async Task<EmployeeDto> DeleteAsync(short id) {
    var toDelete = await _employeeRepository.DeleteAsync(id);
    if (toDelete is null) {
      throw new InvalidOperationException("Could not delete because no record was found for id: " + id);
    }
    return _mapper.Map<EmployeeDto>(toDelete);
  }
}