﻿namespace PU.DataAccess.Entities
{
    public class UserGroup
    {
        public Guid UserGroupId { get; set; }

        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}
