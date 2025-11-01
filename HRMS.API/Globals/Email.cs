using System.Net.Mail;

namespace HRMS.API.Globals
{
    public class Email
    {
        #region Sigleton Pattarn
        private static Email _Instance { get; set; }
        public static Email Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Email();
                return _Instance;
            }
        }
        private Email()
        {

        }

        #endregion
        public bool SendMail(String To, string Subject, string Body)
        {

            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("southbreezesoft@gmail.com");
                mail.To.Add(To);
                //mail.CC.Add(Cc);
                mail.Bcc.Add("ahfcse@gmail.com");
                mail.Subject = Subject;
                mail.Body = Body;
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("southbreezesoft@gmail.com", "vcwyrikiveolrxaz");
                //original pass// SmtpServer.Credentials = new System.Net.NetworkCredential("southbreezesoft@gmail.com", "sbhl2022");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
    }
}
