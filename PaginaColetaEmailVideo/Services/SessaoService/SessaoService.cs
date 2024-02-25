using Newtonsoft.Json;
using PaginaColetaEmailVideo.Models;
using PaginaColetaEmailVideo.Services.Interface;

namespace PaginaColetaEmailVideo.Services.SessaoService;

public class SessaoService : ISessao
{
    private readonly IHttpContextAccessor httpAcessor;

    public SessaoService(IHttpContextAccessor httpAcessor)
    {
        this.httpAcessor = httpAcessor;
    }

    public UsuarioModel BuscarSessao()
    {
        string sessaoUsuario = httpAcessor.HttpContext.Session.GetString("UsuarioAtivo");
        
        if (sessaoUsuario == null)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
    }

    public void CriarSessao(UsuarioModel usuario)
    {
        string usuarioJson = JsonConvert.SerializeObject(usuario);
        
        httpAcessor.HttpContext.Session.SetString("UsuarioAtivo", usuarioJson);
    }

    public void RemoverSessao()
    {
        httpAcessor.HttpContext.Session.Remove("UsuarioAtivo");
    }
}
