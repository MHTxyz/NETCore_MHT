using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Configuration;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                //.AddDeveloperSigningCredential()//开发环境 磁盘 //.AddSigningCredential(...)//生产环境
                .AddSigningCredential(new System.Security.Cryptography.X509Certificates.X509Certificate2(@"C:\Users\tianmh\source\repos\NETCore_MHT\WebApiDemo\AuthServer\证书\socialnetwork.pfx", "MH"))
                .AddTestUsers(new List<IdentityServer4.Test.TestUser> {//InMemoryConfiguration.Users().ToList()
                    new IdentityServer4.Test.TestUser{
                        SubjectId = "1",
                        Username = "mail@qq.com",
                        Password = "password",
                    },
                })
                .AddInMemoryClients(new List<IdentityServer4.Models.Client> {//InMemoryConfiguration.Clients()
                    new IdentityServer4.Models.Client {
                        ClientId = "socialnetwork",
                        ClientSecrets = new [] { new Secret("secret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                        AllowedScopes = {
                            "socialnetwork",//对应
                            //IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                            //IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        }
                    },
                })
                .AddInMemoryApiResources(new List<IdentityServer4.Models.ApiResource> {
                    new IdentityServer4.Models.ApiResource{
                        Name="socialnetwork",
                        DisplayName="社交网络",
                        Scopes={ new Scope("socialnetwork"),},//对应
                     },
                 });

            services.AddMvc();//.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseStaticFiles();

            //app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
