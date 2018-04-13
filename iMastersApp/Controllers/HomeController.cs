using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Somente será acessada se o usuário estiver logado
        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }

        public async Task Logout()
        {
            //Limpa o cookie para finalizar o logout
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}