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
            #region ����
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
//            ���ǿ���ͨ��IHostEnvironment�õ�ApplicationName��Ӧ�ó������ƣ���ContentRootFileProvider����Ŀ����Ŀ¼�ļ��ṩ���򣩡�
//ContentRootPath����Ŀ����Ŀ¼����EnvironmentName��������������WebRootPath��WebRoot����Ŀ¼����WebRootFileProvider��WebRoot�ļ��ṩ����
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
