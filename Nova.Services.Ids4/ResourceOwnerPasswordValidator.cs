using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nova.Services.Ids4
{
    internal class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            await Task.Run(() =>
            {
                if (context.UserName == "admin" && context.Password == "admin888")
                {
                    context.Result = new GrantValidationResult(
                        subject: context.UserName,
                        authenticationMethod: "custom",
                        claims: new Claim[] {
                            new Claim("Name", context.UserName),
                            new Claim("Email", "123@qq.com")
                        });
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "无效的凭证");
                }
            });
        }
    }
}