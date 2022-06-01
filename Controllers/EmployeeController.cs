using Microsoft.AspNetCore.Mvc;
using backend_dotnet.Models;

namespace backend_dotnet.Controllers;

[ApiController]
[Route("/api")]
public class EmployeeController : ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IEmployeeService _employeeService;
    public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
    {
        _logger = logger;
        _employeeService = employeeService;
    }

    [HttpGet, Route("Employees")]
    public async Task<List<Employee>> Get()
    {
        var results = await _employeeService.GetAllAsync();
        return results;
    }
}
