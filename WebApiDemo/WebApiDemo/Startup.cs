using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WebApiDemo.Dtos;
using WebApiDemo.Entities;
using WebApiDemo.Repositories;
using WebApiDemo.Services;

namespace WebApiDemo
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    options.OutputFormatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlDataContractSerializerOutputFormatter());//内容协商 新增支持xml
                });
            //.AddJsonOptions(options=> 
            //{
            //    if (options.SerializerSettings.ContractResolver is Newtonsoft.Json.Serialization.DefaultContractResolver resolver)//返回Json为原样输出【Id,Name】
            //    {
            //        resolver.NamingStrategy = null;
            //    }
            //});


#if DEBUG 
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService,CloudMailService>();
#endif

            var connectionString = Configuration["connectionStrings:productionInfoDbConnectionString"]; //@"Server=(localdb)\MSSQLLocalDB;Database=ProductDB;Trusted_Connection=True";
            services.AddDbContext<MyContext>(options => { options.UseSqlServer(connectionString); });

            services.AddScoped<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, MyContext myContext)
        {
            //日志
            loggerFactory.AddProvider(new NLogLoggerProvider());
            //loggerFactory.AddNLog();//using NLog.Extensions.Logging;
            //ILoggingBuilder b; b.AddNLog();

            //错误页
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            //添加种子数据到数据库
            myContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            //DTO Map
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<Entities.Product, ProductWithoutMaterialDto>();
                cfg.CreateMap<Entities.Product, ProductDto>();
                cfg.CreateMap<Entities.Material, MaterialDto>();
                cfg.CreateMap<ProductCreation, Entities.Product>();
                cfg.CreateMap<Entities.Product, ProductModification>();
                cfg.CreateMap<ProductModification, Entities.Product>();
            });

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
