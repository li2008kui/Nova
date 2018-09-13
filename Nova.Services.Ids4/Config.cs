using IdentityServer4.Models;
using System.Collections.Generic;

namespace Nova.Services.Ids4
{
    public class Config
    {
        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> { };
        }

        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client> { };
        }
    }
}
