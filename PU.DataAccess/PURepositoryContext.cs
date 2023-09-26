using Microsoft.EntityFrameworkCore;
using PU.DataAccess.Entities;

namespace PU.DataAccess
{
    public class PURepositoryContext: DbContext
    {
        #region DbSets
        public DbSet<User> User { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<GroupPermission> GroupPermission { get; set; }
        #endregion
        public PURepositoryContext(DbContextOptions options): base(options)
        {
            
        }
    }
}
