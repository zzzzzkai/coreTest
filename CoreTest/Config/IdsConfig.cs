using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

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
                new Client//身份验证
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
                },
                new Client
                {
                    ClientId = "client2",
                   
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { "https://localhost:5003/signin-oidc" },

                // where to redirect to after logout
                     PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },

                     AllowedScopes = new List<string>
                     {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
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
                },

                new TestUser()
                {
                SubjectId = "2",
                Username = "user2",
                Password = "password",
                Claims = new List<Claim>
                {
                    new Claim("name","name2"),
                    new Claim("webs","http://baidu.com")
                }
            }
            };
        }

        /// <summary>
        /// 通过修改以下属性来添加对标准openid（主题ID）和profile（名字，姓氏等）范围的支持
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

    }
}
