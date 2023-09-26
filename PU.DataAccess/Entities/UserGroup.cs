﻿namespace PU.DataAccess.Entities
{
    public class UserGroup
    {
        public Guid UserGroupId { get; set; }

        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }

        public User User { get; set; }
        public Group Group { get; set; }
    }
}
