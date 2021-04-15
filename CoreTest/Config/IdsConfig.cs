using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Test;

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
                },
                new Client//密码身份校验模式
                {
                    ClientId = "client1",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                }

            };

        public static List<TestUser> TestUsers()
        {
            return new List<TestUser>
            {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "user1",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("name","name1"),
                        new Claim("webs","http://baidu.com")
                    }
                }
            };
        }



    }
}
