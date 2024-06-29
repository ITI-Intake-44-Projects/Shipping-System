namespace ShippingSystem.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string Authorizations { get; set; } = string.Empty;

        public bool Status { get; set; }
    }
}
