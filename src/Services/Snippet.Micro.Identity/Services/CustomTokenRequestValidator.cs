using IdentityServer4.Validation;

namespace Snippet.Micro.Identity.Services
{
    public class CustomTokenRequestValidator : ICustomTokenRequestValidator
    {
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            //context.Result.CustomResponse =
            //  new Dictionary<string, object> { { "patient", "alice" } };
            return Task.CompletedTask;
        }

        public CustomTokenRequestValidator()
        {

        }
    }
}
