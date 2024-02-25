namespace PaginaColetaEmailVideo.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public string Email { get; set; }
    public byte[] SenhaHash { get; set; }
    public byte[] SenhaSalt { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}
