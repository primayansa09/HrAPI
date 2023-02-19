namespace HrAPI.ViewModel
{
    public class LoginToken
    {
        public string? NIK { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int? DepartementId { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpires { get; set; }
    }
}
