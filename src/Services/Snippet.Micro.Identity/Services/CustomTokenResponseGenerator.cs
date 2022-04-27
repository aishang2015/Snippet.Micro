using IdentityServer4.ResponseHandling;
using IdentityServer4.Validation;

namespace Snippet.Micro.Identity.Services
{
    public class CustomTokenResponseGenerator : ITokenResponseGenerator
    {
        public Task<TokenResponse> ProcessAsync(TokenRequestValidationResult validationResult)
        {
            return Task.FromResult(new TokenResponse
            {
                AccessToken = "test"
            });
        }
    }
}
