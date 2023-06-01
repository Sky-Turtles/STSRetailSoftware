namespace STSRetailApi.DTO
{
    public class PostNewEmployeeRequest
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public decimal PayRate { get; set; }
    }
}
