using Microsoft.EntityFrameworkCore;
using PaginaColetaEmailVideo.Data;
using PaginaColetaEmailVideo.Dto;
using PaginaColetaEmailVideo.Models;
using PaginaColetaEmailVideo.Services.Interface;
using System.Security.Cryptography;

namespace PaginaColetaEmailVideo.Services.UsuarioService;

public class UsuarioService : IUsuario
{
    private readonly AppDbContext context;

    public UsuarioService(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<UsuarioModel> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
    {
        try
        {
            CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            var usuario = new UsuarioModel()
            {
                Usuario = usuarioCriacaoDto.Usuario,
                Email = usuarioCriacaoDto.Email,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt
            };

            context.Add(usuario);

            await context.SaveChangesAsync();

            return usuario;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message); 
        }
    }

    public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
    {
        using (HMACSHA512 hmac = new HMACSHA512())
        {
            senhaSalt = hmac.Key;
            senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
        }
    }

    public async Task<UsuarioModel> Login(LoginDto loginDto)
    {
        try
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(user => user.Email == loginDto.Email);

            if (usuario == null)
            {
                return new UsuarioModel();
            }

            if (!VerificarSenha(loginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
            {
                return new UsuarioModel();
            }

            return usuario;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public bool VerificarSenha(string senha, byte[] senhaHash, byte[] senhaSalt)
    {
        using (HMACSHA512 hmac = new HMACSHA512(senhaSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

            return computedHash.SequenceEqual(senhaHash);
        }
    }
}
