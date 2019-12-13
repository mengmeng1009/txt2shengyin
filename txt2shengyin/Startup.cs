using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace txt2shengyin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            #region 跨域
            //services.AddCors(options =>
            //   options.AddPolicy("AllowSameDomain",
            //       builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials())
            //);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
//            我们可以通过IHostEnvironment拿到ApplicationName（应用程序名称）、ContentRootFileProvider（项目所在目录文件提供程序）、
//ContentRootPath（项目所在目录）、EnvironmentName（开发环境）、WebRootPath（WebRoot所在目录）、WebRootFileProvider（WebRoot文件提供程序）
            AppHelper.AppRootDir = env.WebRootPath;
            app.UseRouting();
            //app.UseCors("AllowSameDomain");
            app.UseStaticFiles();
            app.UseFileServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
