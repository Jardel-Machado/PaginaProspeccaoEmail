using PaginaColetaEmailVideo.Models;

namespace PaginaColetaEmailVideo.Services.Interface;

public interface IEmail
{
    Task<EmailModel> SalvarDadosCliente(EmailModel infoRecebida);
    Task<List<EmailModel>> ListarEmails(string pesquisar = null);
    Task<EmailModel> ListarEmailId(int id);
    bool EnviarEmail(string email, string mensagem, string assunto);
}
