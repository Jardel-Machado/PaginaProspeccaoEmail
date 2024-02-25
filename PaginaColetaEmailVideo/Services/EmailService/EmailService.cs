using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PaginaColetaEmailVideo.Data;
using PaginaColetaEmailVideo.Models;
using PaginaColetaEmailVideo.Services.Interface;
using System.Net;
using System.Net.Mail;

namespace PaginaColetaEmailVideo.Services.EmailService;

public class EmailService : IEmail
{
	private readonly AppDbContext context;
	IConfiguration configuration;

    public EmailService(AppDbContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    public bool EnviarEmail(string email, string mensagem, string assunto)
    {
		try
		{
			string host = configuration.GetValue<string>("SMTP:Host");
			string nome = configuration.GetValue<string>("SMTP:Nome");
			string username = configuration.GetValue<string>("SMTP:Username");
			string senha = configuration.GetValue<string>("SMTP:Senha");
			int porta = configuration.GetValue<int>("SMTP:Porta");

			MailMessage mail = new()
			{
				From = new MailAddress(username, nome),
			};

			mail.To.Add(email);
			mail.Subject = assunto;
			mail.Body = mensagem;
			mail.IsBodyHtml = true;
			mail.Priority = MailPriority.High;

			using(SmtpClient smtp = new(host, porta))
			{
				smtp.Credentials = new NetworkCredential(username, senha);
				smtp.EnableSsl = true;
				smtp.Send(mail);
				return true;
			}
		}
		catch (Exception ex)
		{
            throw new Exception(ex.Message);			
        }
    }

    public async Task<EmailModel> ListarEmailId(int id)
    {
		try
		{
			var registroEmail = await context.Emails.FirstOrDefaultAsync(x => x.Id == id);			

			return registroEmail;
		}
		catch (Exception ex)
		{
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<EmailModel>> ListarEmails(string pesquisar = null)
    {
		List<EmailModel> registrosEmails = new();

		try
		{
			if(pesquisar is null)
			{
                registrosEmails = await context.Emails.ToListAsync();
			}
            else
            {
                registrosEmails = await context.Emails.Where(x => x.Nome.Contains(pesquisar) || x.Email.Contains(pesquisar)).ToListAsync();
            }

            return registrosEmails;
		}
		catch (Exception ex)
		{
            throw new Exception(ex.Message);
        }
    }

    public async Task<EmailModel> SalvarDadosCliente(EmailModel infoRecebida)
    {
		try
		{
			context.Add(infoRecebida);

			await context.SaveChangesAsync();

			return infoRecebida;	
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
    }
}
