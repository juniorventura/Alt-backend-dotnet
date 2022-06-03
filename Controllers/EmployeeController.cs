using Microsoft.AspNetCore.Mvc;
using backend_dotnet.Models;
using backend_dotnet.Dtos;

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

    [HttpGet, Route("Employees/{id}")]
    public async Task<ActionResult<Employee>> GetById(short id)
    {
      try {
        var result = await _employeeService.GetByIdAsync(id);
        return Ok(result);
      } catch (InvalidOperationException ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost, Route("Employee")]
    public async Task<ActionResult<Employee>> Post([FromBody] EmployeeDto employeeDto)
    {
      try {
        var result = await _employeeService.CreateAsync(employeeDto);
        return Ok(result);
      } catch (InvalidOperationException ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut, Route("Employee")]
    public async Task<ActionResult<EmployeeDto>> Edit([FromBody] EmployeeDto employee)
    {
      try {
        var result = await _employeeService.EditAsync(employee);
        return Ok(result);
      } catch (InvalidOperationException ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpDelete, Route("Employee/{id}")]
    public async Task<ActionResult<EmployeeDto>> Delete(short id)
    {
      try {
        var result = await _employeeService.DeleteAsync(id);
        return Ok(result);
      } catch (InvalidOperationException ex) {
        return BadRequest(ex.Message);
      }
    }
}
