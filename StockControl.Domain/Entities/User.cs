namespace StockControl.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin = 1,
        Seller = 2
    }
}
