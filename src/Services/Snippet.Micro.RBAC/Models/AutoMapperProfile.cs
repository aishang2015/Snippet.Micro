using AutoMapper;
using Snippet.Micro.RBAC.Data.Entity.RBAC;
using Snippet.Micro.RBAC.Models.RBAC.Element;
using Snippet.Micro.RBAC.Models.RBAC.Organization;
using Snippet.Micro.RBAC.Models.RBAC.Role;
using Snippet.Micro.RBAC.Models.RBAC.User;

namespace Snippet.Micro.RBAC.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Element, GetElementOutputModel>();
            CreateMap<CreateElementInputModel, Element>();
            CreateMap<UpdateElementInputModel, Element>();

            CreateMap<Organization, GetOrganizationOutputModel>();
            CreateMap<CreateOrganizationInputModel, Organization>();
            CreateMap<UpdateOrganizationInputModel, Organization>();

            CreateMap<SnippetAdminRole, GetRoleOutputModel>();
            CreateMap<AddOrUpdateRoleInputModel, SnippetAdminRole>();

            CreateMap<AddOrUpdateUserInputModel, SnippetAdminUser>();
            CreateMap<SnippetAdminUser, GetUserOutputModel>();
        }
    }
}