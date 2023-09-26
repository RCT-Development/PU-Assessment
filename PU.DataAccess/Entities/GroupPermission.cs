namespace PU.DataAccess.Entities
{
    public class GroupPermission
    {
        public Guid GroupPermissionId { get; set; }

        public Guid GroupId { get; set; }
        public Guid PermissionId { get; set; }

        public Group Group { get; set; }
        public Permission Permission { get; set; }
    }
}
