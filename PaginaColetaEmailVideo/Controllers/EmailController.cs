using Microsoft.AspNetCore.Mvc;
using PaginaColetaEmailVideo.Filtros;
using PaginaColetaEmailVideo.Models;
using PaginaColetaEmailVideo.Services.Interface;

namespace PaginaColetaEmailVideo.Controllers;

[UsuarioLogado]
public class EmailController : Controller
{
    private readonly IEmail emailInterface;

    public EmailController(IEmail emailInterface)
    {
        this.emailInterface = emailInterface;
    }

    public async Task<ActionResult<List<EmailModel>>> Index(string? pesquisar)
    {
        if(pesquisar != null) 
        {
            var registrosEmailFiltro = await emailInterface.ListarEmails(pesquisar);
            
            return View(registrosEmailFiltro);
        }        
        
        var registrosEmails = await emailInterface.ListarEmails();

        return View(registrosEmails);
    }

    [HttpGet]
    public async Task<ActionResult<EmailModel>> DetalhesEmail(int id)
    {
        var registroEmail = await emailInterface.ListarEmailId(id);

        return View(registroEmail);
    }
    
    [HttpPost]
    public async Task<ActionResult<EmailModel>> EnviarEmail(string enderecoEmail, string textoEmail, string assuntoEmail, int id)
    {
        var email = await emailInterface.ListarEmailId(id);

        if (email.Status is false)
        {
            TempData["MensagemErro"] = "Não é possível encaminhar email para um registro inativo!";

            return View("DetalhesEmail", email);
        }

        if(textoEmail is null || assuntoEmail is null)
        {
            TempData["MensagemErro"] = "Insira um assunto e um corpo para o email!";

            return View("DetalhesEmail", email);
        }

        bool resultado =  emailInterface.EnviarEmail(enderecoEmail, textoEmail, assuntoEmail);

        if(resultado is true)
        {
            TempData["MensagemSucesso"] = "Email encaminhado com sucesso!";
        }
        else
        {
            TempData["MensagemErro"] = "Ocorreu um problema no envio do email";
        }

        return Redirect("Index");
    }
}
