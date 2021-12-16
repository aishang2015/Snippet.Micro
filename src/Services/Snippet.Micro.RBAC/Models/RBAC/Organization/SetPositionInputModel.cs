﻿namespace Snippet.Micro.RBAC.Models.RBAC.Organization
{
    public class SetPositionInputModel
    {
        public int OrganizationId { get; set; }

        public PositionModel[] Positions { get; set; }
    }

    public class PositionModel
    {
        public bool VisibleToChild { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}