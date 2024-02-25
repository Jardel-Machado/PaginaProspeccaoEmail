using System.ComponentModel.DataAnnotations;

namespace PaginaColetaEmailVideo.Dto;

public class UsuarioCriacaoDto
{
    [Required(ErrorMessage = "Insira um usuário!")]
    public string Usuario { get; set; }

    [Required(ErrorMessage = "Insira um Email!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Insira um Senha!")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "Insira a confirmação de senha!"), Compare("Senha", ErrorMessage = "Senhas estão diferentes!")]
    public string ConfirmaSenha { get; set; }
}
