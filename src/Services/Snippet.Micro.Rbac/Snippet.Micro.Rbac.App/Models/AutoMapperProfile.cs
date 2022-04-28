using AutoMapper;
using Snippet.Micro.Rbac.App.Data.Entity.RBAC;
using Snippet.Micro.Rbac.App.Models.RBAC.Element;
using Snippet.Micro.Rbac.App.Models.RBAC.Organization;
using Snippet.Micro.Rbac.App.Models.RBAC.Role;
using Snippet.Micro.Rbac.App.Models.RBAC.User;

namespace Snippet.Micro.Rbac.App.Models
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