namespace Snippet.Micro.RBAC.Models.RBAC.Account
{
    public class GetCurrentUserInfoOutputModel
    {
        public string UserName { get; set; }
        public string[] Identities { get; set; }
    }
}
