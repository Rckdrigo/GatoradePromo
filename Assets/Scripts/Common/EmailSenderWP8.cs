using System.Collections;
using System;
using System.IO;
using UnityEngine;	
using Microsoft.Phone.Tasks;

namespace MyUnity.CommonUtilities{
	public sealed class EmailSenderWP8 {
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
		
		public static bool sendEmail (){//string attachment){
			EmailComposeTask emailComposeTask = new EmailComposeTask();
			emailComposeTask.Subject = _ebody.subject;
			emailComposeTask.Body =  _ebody.body;
			emailComposeTask.To = _ebody.to;
			
			emailComposeTask.Show();
			/*MailMessage mail = new MailMessage();
			
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
			return true;*/
			
			return true;
		}

	}
}
