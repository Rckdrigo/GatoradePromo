using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MyUnity.CommonUtilities{
	public sealed class EmailSender{

		struct EmailBody{
			public string from;
			public string to;
			public string subject;
			public string body;
		}
		struct EmailCredentials{
			public string credentialsEmail;
			public string credentialsPassword;
		}
		static EmailCredentials _credentials;
		static EmailBody _ebody;

		public static void SetEmailBody(string from, string to, string subject, string body){
			_ebody.from = from;
			_ebody.to = to;
			_ebody.subject = subject;
			_ebody.body = body;
		}

		public static void SetCredentials(string email, string password){
			_credentials.credentialsEmail = email;
			_credentials.credentialsPassword = password;
		}

		static bool sendEmail (string attachment){
			MailMessage mail = new MailMessage();
			
			mail.From = new MailAddress(_ebody.from);
			mail.To.Add(_ebody.to);
			mail.Subject = _ebody.subject;
			mail.Body = _ebody.body;

			mail.Attachments.Add(new System.Net.Mail.Attachment(attachment));

			SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
			smtpServer.Port = 587;
			smtpServer.Credentials = new System.Net.NetworkCredential(_credentials.credentialsEmail, _credentials.credentialsPassword) 
				as ICredentialsByHost;
			smtpServer.EnableSsl = true;
			ServicePointManager.ServerCertificateValidationCallback = 
				delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
			{ return true; };
			smtpServer.Send(mail);
			return true;
		}

		static bool sendEmail (){
			MailMessage mail = new MailMessage();
			
			mail.From = new MailAddress(_ebody.from);
			mail.To.Add(_ebody.to);
			mail.Subject = _ebody.subject;
			mail.Body = _ebody.body;
			
			SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
			smtpServer.Port = 587;
			smtpServer.Credentials = new System.Net.NetworkCredential(_credentials.credentialsEmail, _credentials.credentialsPassword) 
				as ICredentialsByHost;
			smtpServer.EnableSsl = true;
			ServicePointManager.ServerCertificateValidationCallback = 
				delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
			{ return true; };
			smtpServer.Send(mail);
			return true;
		}

		public static IEnumerator SendEmail(){
			yield return EmailSender.sendEmail();
		}

		public static IEnumerator SendEmail(string filename){
			yield return EmailSender.sendEmail(filename);
		}
	}
}
