namespace TrashCollectionAPI.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string? Role { get; set; }
    }
}
