using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        // Aqui vamos definir os resources que serão utilizados no nosso servidor
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(                 name: "custom.profile",                 displayName: "Custom Profile",                 claimTypes: new[] { "customclaim" }             );

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                customProfile
            };
        }

        // Aqui definimos qual Client (aplicação) que poderá acessar nosso servidor de identidade.
        public static IEnumerable<Client> GetClients()
        {
            // Credenciais da Aplicação
            return new List<Client>
            {
                // OpenID Connect
                new Client
                {
                    // O Nome ÚNICO da nossa aplicação autorizada no nosso servidor de autoridade
                    ClientId = "iMastersApp",
                    
                    // Nome de exibição da nossa aplicação
                    ClientName = "iMasters Application",
                    
                    //Tipo de autenticação permitida
                    AllowedGrantTypes = GrantTypes.Implicit,

                    //Url de redicionamento para quando o login for efetuado com sucesso.
                    RedirectUris = { "http://localhost:5001/signin-oidc" },

                    //Url de redirecionamento para quando o logout for efetuado com sucesso.
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

                    //Escopos permitidos dentro da aplicação
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "custom.profile"
                    }
                }
            };
        }

        // TestUser é uma classe de exemplo do proprio IdentityServer, aonde configuramos basicamente login/senha e as claims de exibição.
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "imasters",
                    Password = "imasters",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Portal iMasters"),
                        new Claim("website", "https://imasters.com.br"),
                        new Claim("email", "contato@imasters.com"),
                        new Claim("customclaim", "Minha claim customizada")
                    }
                }
            };
        }
    }
}
