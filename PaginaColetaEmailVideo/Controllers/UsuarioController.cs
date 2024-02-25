using Microsoft.AspNetCore.Mvc;
using PaginaColetaEmailVideo.Dto;
using PaginaColetaEmailVideo.Filtros;
using PaginaColetaEmailVideo.Models;
using PaginaColetaEmailVideo.Services.Interface;

namespace PaginaColetaEmailVideo.Controllers;

[UsuarioLogado]
public class UsuarioController : Controller
{
    private readonly IUsuario usuarioInterface;

    public UsuarioController(IUsuario usuarioInterface)
    {
        this.usuarioInterface = usuarioInterface;
    }

    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioModel>> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
    {
        if(ModelState.IsValid) 
        {
            var usuario = await usuarioInterface.Cadastrar(usuarioCriacaoDto);

            if(usuario != null) 
            {
                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Index", "Email");
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro no momento do cadastro!";
                return View(usuarioCriacaoDto);
            }
        }
        else
        {
            return View(usuarioCriacaoDto);
        }
    }

}
