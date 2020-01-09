using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[]
            {
                new ApiResource("socialnetwork", "社交网络")
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "socialnetwork",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "socialnetwork" }
                }
            };
        }

        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    UserName = "mail@qq.com",
                    Password = "password"
                }
            };
        }
    }

    public class ApiResource
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public ApiResource( string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }
    }


    public class Client
    {
        public string ClientId { get; set; }
        public Secret[] ClientSecrets { get; set; }
        public ICollection<string> AllowedGrantTypes { get; set; }
        public string[] AllowedScopes { get; set; }
    }


    public class TestUser
    {
        public string SubjectId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
