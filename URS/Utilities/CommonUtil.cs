using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Xml;

namespace URS.Utilities
{    
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
    public static class CommonUtil
    {
        
        public static string DateFormatUs = "MM/dd/yyyy";
        public static string DateTimeFormatUs = "MM/dd/yyyy HH:mm:ss tt";

        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetEnumDesc(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static string GetEnumDisplayName(this Enum enumValue, Type enumType)
        {
            return enumType.GetMember(enumValue.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }

        public static string XmlDocToString(this XmlDocument doc)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    doc.WriteTo(xmlTextWriter);
                    return stringWriter.ToString();
                }
            }
        }

        public static XmlDocument StringToXmlDoc(this string xmlAsString)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlAsString);
                return xmlDoc;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public static void SendEmail(string subject, string toEmail, string body, String displayName)
        {
            try
            {
                using (MailMessage msgMail = new MailMessage())
                {
                    msgMail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                    string toAdd = string.Empty;
                    toAdd = toEmail;
                    msgMail.To.Add(toAdd);

                    msgMail.From = new MailAddress(ConfigurationManager.AppSettings.Get("FROM_EMAIL"), displayName);
                    msgMail.Subject = subject;

                    msgMail.IsBodyHtml = true;
                    string strBody = body;
                    msgMail.Body = strBody;

                    SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings.Get("SMTP_Server"));
                    bool isGmailUsedToSendMail = ConfigurationManager.AppSettings.Get("IsGmailUsedToSendMail") == "1";

                    if (!isGmailUsedToSendMail)
                    {
                        smtp.UseDefaultCredentials = false;
                    }
                    else
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Port = 25;
                        smtp.EnableSsl = false;
                    }
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings.Get("Mail_USER_ID"), ConfigurationManager.AppSettings.Get("Mail_PASSWORD"));
                    smtp.Send(msgMail);            
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static string Encrypt(string pass)
        {
            string encpass = "";
            try
            {
                byte[] s = System.Text.ASCIIEncoding.ASCII.GetBytes(pass);
                encpass = Convert.ToBase64String(s);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return encpass;
        }
        public static string Decrypt(string pass)
        {
            string depass = "";
            try
            {
                byte[] s = Convert.FromBase64String(pass);
                depass = System.Text.ASCIIEncoding.ASCII.GetString(s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return depass;
        }

        public static double GramToPound(double gram)
        {
            return gram * 0.0022046;
        }

        public static string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
