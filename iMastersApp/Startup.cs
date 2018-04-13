using System.IdentityModel.Tokens.Jwt;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iMastersApp
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

            //Exibe as claims de maneira mais "amigável"
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //Adciona o serviço de autenticação
            services.AddAuthentication(options =>
            {
                //Nosso esquema default será baseado em cookie
                options.DefaultScheme = "Cookies";

                //Como precisamos recuperar os dados depois do login, utilizamos o OpenID Connect que por padrão utiliza o escopo do Profile
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";
                    
                    //Aponta para o nosso servidor de autenticação
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    
                    //Nome da nossa aplicação que tentará se autenticar no nosso servidor de identidade
                    //Observe que ela possui o mesmo nome da app que liberamos no nosso servidor de identidade
                    options.ClientId = "iMastersApp";
                    options.SaveTokens = true;
                    
                    //Adicionamos o scopo do e-mail para utilizarmos a claim de e-mail.
                    options.Scope.Add(IdentityServerConstants.StandardScopes.Email);

                    options.Scope.Add("custom.profile");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Habilitando o uso da autenticação do Identity Server no nosso projeto
            app.UseAuthentication();

            //Habilitando o uso de arquivos estáticos (Html, Css e etc) do nosso projeto
            app.UseStaticFiles();

            //Habilitando o uso de rota no projeto
            app.UseMvcWithDefaultRoute();
        }
    }
}
