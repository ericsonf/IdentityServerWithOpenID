using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
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
            services.AddMvc();

            // Configuração do Identity Server In-Memory
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //Habilitando o uso do Identity Server no nosso projeto
            app.UseIdentityServer();

            //Habilitando o uso de arquivos estáticos (Html, Css e etc) do nosso projeto
            app.UseStaticFiles();

            //Habilitando o uso de rota no projeto
            app.UseMvcWithDefaultRoute();
        }
    }
}
