namespace UserAPIDemo.DTOs
{
    public class CreateUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? CreatedBy { get; set; } = 1;
    }
}
