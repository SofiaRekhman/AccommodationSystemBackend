namespace DAL.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}

