using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace CoreTest.Config
{
    /// <summary>
    /// 注册的验证
    /// </summary>
    public static class IdsConfig
    {
        /// <summary>
        /// 作用域
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };


        public static IEnumerable<ApiScope> ApiScope => 
            new List<ApiScope>
            {
                new ApiScope("api1","API1")
            };
        /// <summary>
        /// 客户端接入的模式
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                }
            };




    }
}
