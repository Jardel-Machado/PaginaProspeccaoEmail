using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using PaginaColetaEmailVideo.Models;

namespace PaginaColetaEmailVideo.Filtros;

public class UsuarioLogado : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        string SessaoUsuario = context.HttpContext.Session.GetString("UsuarioAtivo");

        if(string.IsNullOrEmpty(SessaoUsuario))
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                {"controller", "Home" },
                {"Action", "Index"}
            });
        }
        else
        {
            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(SessaoUsuario);

            if(usuario is null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "Home" },
                    {"Action", "Index"}
                });
            }
        }
        
        base.OnActionExecuted(context);
    }
}
