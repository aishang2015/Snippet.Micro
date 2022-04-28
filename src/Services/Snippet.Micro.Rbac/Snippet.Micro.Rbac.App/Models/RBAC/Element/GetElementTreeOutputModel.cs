﻿namespace Snippet.Micro.Rbac.App.Models.RBAC.Element
{
    public class GetElementTreeOutputModel
    {
        public string Title { get; set; }

        public int Type { get; set; }

        public int Key { get; set; }

        public List<GetElementTreeOutputModel> Children { get; set; }
    }
}