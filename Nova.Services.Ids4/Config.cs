using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Nova.Services.Ids4
{
    public class Config
    {
        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource("Ticket.API", "票务接口")
            };
        }

        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = "C_ID_1",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime = 10,
                    ClientSecrets =
                    {
                        new Secret("C_SECRET_1".Sha256())
                    },
                    AllowedScopes =
                    {
                        "Ticket.API",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}
