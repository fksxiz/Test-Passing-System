using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace QQCourse
{
    public class EmailMessageSender
    {
        public enum MessageType
        {
            AUTHENTIFICATION,
            CONFIRMATION,
            REQUEST_INFO
        }

        private string smtpServer = "smtp.mail.ru"; //smtp сервер
        private int smtpPort = 587; //port
        private string smtpUsername = "testpassingsystem@mail.ru"; //Имя почты для отправки сообщений
        private string smtpPassword = "ER1zGG1jB7ytRd4BL9Ff"; //Пароль почты для внешнего приложения
        private MessageType type;
        public EmailMessageSender(MessageType type) {
            this.type = type;
        }

        public bool SendMessage(string recipient, string testName="", string requestStatus="")
        {
            using(SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials=new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From=new MailAddress(smtpUsername);
                    mailMessage.To.Add(recipient);
                    Core.VereficationCode = GenerateCode();
                    switch (type)
                    {
                        case MessageType.AUTHENTIFICATION:
                            mailMessage.Subject = "Подтверждение входа в систему тестирования знаний.";
                            mailMessage.Body = $"Подтверждение входа в систему тестирования знаний.\n\r********************************************\n\rКод подтверждения: {Core.VereficationCode}\n\r********************************************\n\rЭто письмо создано автоматически, отвечать на него не нужно.\n\rЕсли это не вы пытаетесь войти в аккаунт, то немедленно поменяйте данные своей учётной записи.";
                            break;
                        case MessageType.CONFIRMATION:
                            mailMessage.Subject = "Подтверждение адреса электронной почты в системе тестирования знаний.";
                            mailMessage.Body = $"Подтверждение адреса электронной почты в системе тестирования знаний.\n\r********************************************\n\rКод подтверждения: {Core.VereficationCode}\n\r********************************************\n\rЭто письмо создано автоматически, отвечать на него не нужно.\n\rЕсли вы считаете что это сообщение пришло вам по ошибке, то просто удалите его.";
                            break;
                        case MessageType.REQUEST_INFO:
                            mailMessage.Subject = $"Ваша заявка на перепрохождение теста {testName} была {requestStatus}";
                            mailMessage.Body = $"Ваша заявка на перепрохождение теста {testName} была {requestStatus}!\n\rЭто письмо создано автоматически, отвечать на него не нужно.\n\rЕсли вы считаете что это сообщение пришло вам по ошибке, то просто удалите его.";
                            break;
                    }

                    try
                    {
                        smtpClient.Send(mailMessage);
                        //smtpClient.SendMailAsync(mailMessage);
                        return true;
                    }catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return false;
                    }
                }
            }
        }

        private string GenerateCode()
        {
            string Digits = "1234567890";
            string Alphabet = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string code = "";
            Random generator = new Random();
            code += Alphabet[generator.Next(0, Alphabet.Length)]+"-";
            for(int i = 0; i<6; i++)
            {
                code += Digits[generator.Next(0, Digits.Length)];
            }
            return code;
        }

    }
}
