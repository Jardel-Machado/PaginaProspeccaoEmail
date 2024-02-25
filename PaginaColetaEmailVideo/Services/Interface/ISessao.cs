using PaginaColetaEmailVideo.Models;

namespace PaginaColetaEmailVideo.Services.Interface;

public interface ISessao
{
    UsuarioModel BuscarSessao();
    void CriarSessao(UsuarioModel usuario);
    void RemoverSessao();
}
