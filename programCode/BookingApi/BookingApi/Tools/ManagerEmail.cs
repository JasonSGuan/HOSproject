using System;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApi.Tools
{
    public class ManagerEmail
    {
        public class EMail
        {
            private SmtpClient smtpClient;
            // 实例化一个邮件类
            MailMessage _mailMessage = new MailMessage();
            /// <summary>
            /// 发件使用的邮箱地址
            /// </summary>
            private string _fromEmail { set; get; }

            /// <summary>
            /// 发件邮箱地址密码
            /// </summary>
            private string _senderPassword { set; get; }

            /// <summary>
            /// 显示在邮件上的发件人名称
            /// </summary>
            private string _displayName { set; get; }

            /// <summary>
            /// 发件邮箱使用的服务器
            /// </summary>
            private string _smtp { set; get; }

            /// <summary>
            /// 发件邮箱使用的服务器端口
            /// </summary>
            private string _smtpCom { set; get; }

            /// <summary>
            /// 邮件的接收者
            /// </summary>
            private string ToEmail { set; get; }

            /// <summary>
            /// 抄送
            /// </summary>
            private string CC { set; get; }

            /// <summary>
            /// 密送
            /// </summary>
            private string Bcc { set; get; }

            /// <summary>
            /// 邮件标题
            /// </summary>
            private string Subject { set; get; }

            /// <summary>
            /// 邮件正文
            /// </summary>
            private string Body { set; get; }
            /// <summary>
            /// 邮件正文
            /// </summary>
            private bool enableSsl { set; get; }

            /// <summary>
            /// 附件
            /// </summary>
            private string Attachments { set; get; }

            /// <summary>
            /// 附件
            /// </summary>
            private string ContentId { set; get; }

            public EMail(string toEmail, string subject, string body)
            {
                ToEmail = toEmail;
                Subject = subject;
                Body = body;
            }

            public EMail(string toEmail, string cc, string bcc, string subject, string body, string attachments,
                        string displayName, string fromEmail, string senderPassword, string smtp, string smtpCom, string contentId, bool EnableSsl)
            {

                CC = cc;
                Bcc = bcc;
                ToEmail = toEmail;
                Subject = subject;
                Body = body;
                Attachments = attachments;
                _displayName = displayName;
                _fromEmail = fromEmail;
                _senderPassword = senderPassword;
                _smtp = smtp;
                _smtpCom = smtpCom;
                ContentId = contentId;
                enableSsl = EnableSsl;
            }

            /// <summary>
            /// 发送email
            /// </summary>
            /// <param name="toEmail">邮件的接收者，支持群发，多个地址之间用半角逗号分开</param>    
            /// <param name="subject">主题</param>
            /// <param name="body">正文</param>    
            public static bool SendEmail(string toEmail, string subject, string body)
            {
                EMail email = new EMail(toEmail, subject, body);
                return email.Send();
            }

            /// <summary>
            /// 发送email
            /// </summary>
            /// <param name="toEmail">邮件的接收者，支持群发，多个地址之间用半角逗号分开</param>
            /// <param name="cc">抄送，多个地址之间用半角逗号分开</param>
            /// <param name="bcc">密送，多个地址之间用半角逗号分开</param>
            /// <param name="subject">主题</param>
            /// <param name="body">正文</param>
            /// <param name="attachments">附件地址.多个附件的文件名称我用 | 隔开的</param>
            ///  <param name="displayName">发件人昵称</displayName>
            public static bool SendEmail(string toEmail, string cc, string bcc, string subject, string body, string attachments,
                                        string displayName, string fromEmail, string senderPassword, string smtp, string smtpCom, string strContentId, bool EnableSsl)
            {
                EMail email = new EMail(toEmail, cc, bcc, subject, body, attachments, displayName, fromEmail, senderPassword, smtp, smtpCom, strContentId, EnableSsl);
                return email.Send();
            }

            /// <summary>
            /// 邮件发送
            /// </summary>
            public bool Send()
            {
                Type type = typeof(SmtpClient);
                SmtpClient _smtpClient = new SmtpClient();

                bool result = false;

                try
                {
                    // 将smtp的出站方式设为Network
                    _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    // smtp服务器是否启用SSL加密
                    _smtpClient.EnableSsl = enableSsl;

                    // 指定smtp服务器地址
                    _smtpClient.Host = _smtp;

                    // 指定smtp服务器的端口，默认是25，如果采用默认端口，可省去
                    _smtpClient.Port = Convert.ToInt32(_smtpCom);             

                    // 如果你的SMTP服务器不需要身份认证，则使用下面的方式，不过，目前基本没有不需要认证的了
                    //smtp.UseDefaultCredentials = true;
                    _smtpClient.UseDefaultCredentials = true;

                    // 如果需要认证，则用下面的方式
                    _smtpClient.Credentials = new NetworkCredential(_fromEmail, _senderPassword);

                    // 邮件的优先级，分为Low、Normal、 High，通常用Normal即可
                    _mailMessage.Priority = MailPriority.Normal;

                    // 收件方看到的邮件来源；第一个参数是发信人邮件地址，第二参数是发信人显示的名称，第三个参数是第二个参数所使用的编码，如果指定不正确，则对方收到后显示乱码，936是简体中文的codepage值
                    _mailMessage.From = new MailAddress(_fromEmail, _displayName, Encoding.GetEncoding(936));

                    // ReplyTo 表示对方回复邮件时默认的接收地址，即：你用一个邮箱发信，但却用另一个来收信，后两个参数的意义，同From
                    //_mailMessage.ReplyTo = new MailAddress(_fromEmail, _displayName, Encoding.GetEncoding(936));

                    // 邮件的接收者，支持群发，多个地址之间用半角逗号分开
                    _mailMessage.To.Add(ToEmail);

                    // 抄送
                    if (!string.IsNullOrEmpty(CC))
                    {
                        _mailMessage.CC.Add(CC);
                    }

                    // 密送
                    if (!string.IsNullOrEmpty(Bcc))
                    {
                        _mailMessage.Bcc.Add(Bcc);
                    }

                    // 邮件标题
                    _mailMessage.Subject = Subject;

                    // 这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码
                    _mailMessage.SubjectEncoding = Encoding.GetEncoding(936);

                    // 邮件正文是否是HTML格式
                    _mailMessage.IsBodyHtml = true;



                    // 邮件正文的编码，设置不正确，接收者会收到乱码
                    _mailMessage.BodyEncoding = Encoding.GetEncoding(936);


                    // 附件
                    if (Attachments.Length != 0)//发送附件（多个附件的文件名称我用 | 隔开的，所以此处这样写）
                    {
                        string[] arrfile = Attachments.Split('|');
                        for (int i = 0; i < arrfile.Length; i++)
                        {
                            if (arrfile[i].Length > 0)
                            {
                                if (File.Exists(arrfile[i]))//判断文件存在就添加
                                { _mailMessage.Attachments.Add(new Attachment(arrfile[i])); }
                                else//如果不存在就跳出本次循环
                                { continue; }
                            }
                        }
                    }

                    //原版单一附件
                    //Attachment data = new Attachment(Attachments);
                    //_mailMessage.Attachments.Add(data);

                    // 邮件正文
                    _mailMessage.Body = Body;
                    _smtpClient.Timeout = 1000000;
                    // 发送邮件
                    _smtpClient.Send(_mailMessage);
                    //释放资源
                    _mailMessage.Dispose();

                    result = true;
                }
                catch (Exception ex)
                {
                    //释放资源
                    _mailMessage.Dispose();
                    Console.WriteLine(string.Format("错误原因:{0}", ex));
                    result = false;
                }

                return result;
            }
        }

    }
}
