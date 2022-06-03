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

  public async  Task<EmployeeDto> GetByIdAsync(short employeeId) {

    if (employeeId > 0) {
      var employee = await _employeeRepository.GetByIdAsync(employeeId);
      if (employee is null) {
        throw new InvalidOperationException("Employee was not found");
      }
      return new EmployeeDto {
        EmployeeId = employee.EmployeeId,
        Title = employee.Title,
        LastName = employee.LastName,
        FirstName = employee.FirstName
      };
    }
    else {
      throw new InvalidOperationException("EmployeeId must be greater than 0.");
    }
  }

  public async  Task<Employee> CreateAsync(EmployeeDto employee) {

    // Checking the data that comes from the client

    if (string.IsNullOrEmpty(employee.FirstName) 
    || string.IsNullOrEmpty(employee.LastName) 
    || string.IsNullOrEmpty(employee.Title)) {
      throw new InvalidOperationException("Required fields are missing.");
    }

    // Business rule 1

    var existingEmployee = await _employeeRepository.GetByNameAndLastNameAsync(employee.FirstName, employee.LastName);

    if (existingEmployee is not null) {
      throw new InvalidOperationException("There is an existing user with that name and lastname.");
    }

    // This is just to grab the properties :)
    existingEmployee = await _employeeRepository.GetWithMaxId();

    short nextId;
    if (existingEmployee is null) nextId = 1;
    else nextId = (short)(existingEmployee.EmployeeId + 1);

    
    // mapping or dto to an actual entity without automapper :(

    /*var newEmployee = new Employee {
      EmployeeId = (short)(existingEmployee.EmployeeId + 1),
      Address = existingEmployee.Address,
      BirthDate = existingEmployee.BirthDate,
      City = existingEmployee.City,
      Country = existingEmployee.Country,
      Extension = existingEmployee.Extension,
      FirstName = employee.FirstName,
      HireDate = existingEmployee.HireDate,
      HomePhone = existingEmployee.HomePhone,
      LastName = employee.LastName,
      Region = existingEmployee.Region,
      Title = employee.Title,
      Notes = existingEmployee.Notes,
      Photo = existingEmployee.Photo,
      PhotoPath = existingEmployee.PhotoPath,
      PostalCode = existingEmployee.PostalCode,
      ReportsTo = existingEmployee.ReportsTo,
      TitleOfCourtesy = existingEmployee.TitleOfCourtesy,
    };*/
    
    var newEmployee = _mapper.Map<Employee>(existingEmployee);
    newEmployee.EmployeeId = nextId;
    newEmployee.FirstName = employee.FirstName;
    newEmployee.LastName = employee.LastName;

    return await _employeeRepository.Create(newEmployee);
  }

  public async  Task<Employee> EditAsync(EmployeeDto Employee) {
    if (Employee.EmployeeId <= 0) {
      throw new InvalidOperationException();
    }
    var existingEmployee = await _employeeRepository.GetByIdAsync(Employee.EmployeeId);

    if (existingEmployee is null) {
      throw new InvalidOperationException("employee was not found with id: " + Employee.EmployeeId);
    }

    existingEmployee.LastName = Employee.LastName;
    existingEmployee.FirstName = Employee.FirstName;
    existingEmployee.Title = Employee.Title;

    return await _employeeRepository.EditAsync(existingEmployee);
  }

  public async Task<Employee> DeleteAsync(short Id) {
    var deleted = await _employeeRepository.DeleteAsync(Id);
    if (deleted is null) {
      throw new InvalidOperationException("Could not delete because no record was found for id: " + Id);
    }
    return deleted;
  }

}