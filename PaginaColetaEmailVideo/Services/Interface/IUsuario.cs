using PaginaColetaEmailVideo.Dto;
using PaginaColetaEmailVideo.Models;

namespace PaginaColetaEmailVideo.Services.Interface;

public interface IUsuario
{
    Task<UsuarioModel> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto);
    Task<UsuarioModel> Login(LoginDto loginDto);
}
