namespace backend_dotnet.Dtos
{
    public class EmployeeDto
    {
        public short EmployeeId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Title { get; set; }
    }
}
