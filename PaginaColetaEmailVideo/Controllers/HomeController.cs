using Microsoft.AspNetCore.Mvc;
using PaginaColetaEmailVideo.Dto;
using PaginaColetaEmailVideo.Models;
using PaginaColetaEmailVideo.Services.Interface;
using System.Diagnostics;

namespace PaginaColetaEmailVideo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmail emailInterface;

        private readonly IUsuario usuarioInterface;

        private readonly ISessao sessaoInterface;

        public HomeController(IEmail emailInterface, IUsuario usuarioInterface, ISessao sessaoInterface)
        {
            this.emailInterface = emailInterface;
            this.usuarioInterface = usuarioInterface;
            this.sessaoInterface = sessaoInterface;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Agradecimento(EmailModel infoRecebida)
        {
            return View(infoRecebida);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Sair()
        {
            sessaoInterface.RemoverSessao();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> SalvarDadosCliente(EmailModel infoRecebida)
        {
            if(ModelState.IsValid) 
            {
                var registroFeito = await emailInterface.SalvarDadosCliente(infoRecebida);

                return View("Agradecimento", infoRecebida);
            }
            else
            {
                return RedirectToAction("Index");
            }            
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Login(LoginDto loginDto)
        {
            if(ModelState.IsValid) 
            {
                var usuario = await usuarioInterface.Login(loginDto);

               if(usuario.Id == 0)
               {
                    TempData["MensagemErro"] = "Credenciais inválidas!";

                    return View(loginDto);
               }
               else
               {
                    TempData["MensagemSucesso"] = "Usúário logado com sucesso!";

                    sessaoInterface.CriarSessao(usuario);

                    return RedirectToAction("Index", "Email");
               }
            }
            else
            {
                return View(loginDto);
            }            
        }

    }
}
