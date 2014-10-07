using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.IO;

namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create network credentials to access your SendGrid account
            var username = "Your sendgrid username";
            var passwd = "Your sendgrid password";

            //create the reply to email address
            MailAddress[] replyToAddress = new MailAddress[2];
            replyToAddress[0] = new MailAddress("justnisar@gmail.com");
            replyToAddress[1] = new MailAddress("john.nimmy@gmail.com");

           
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();

            // Add the message properties.
            //From 
              myMessage.From = new MailAddress("john.nimmy@gmail.com", "Nimmy John");

            // Add multiple addresses to the To field.
            List<String> recipients = new List<String>
            {
                @"Mridul Mundara <mrdlmndr@gmail.com>"
            };

            myMessage.AddTo(recipients);
            //Subject 
            myMessage.Subject = "Testing the SendGrid Library";
            //Mail Content Type 
            //Add the HTML and Text bodies
            myMessage.Html = "<p><a href=\"http://www.example.com\">Hello World Link!</a></p>";
            myMessage.Text = "Hello World!";
            
       //BCC
          // myMessage.EnableBcc("john.nimmy@gmail.com");
            //Reply Address
           myMessage.ReplyTo = replyToAddress;
            // Add a footer to the message.
            myMessage.EnableFooter("PLAIN TEXT FOOTER", "<p><em>HTML FOOTER</em></p>");

            using (var attachmentFileStream = new FileStream(@"C:\Users\Public\Pictures\Sample Pictures\Desert.jpg", FileMode.Open))
            {
                myMessage.AddAttachment(attachmentFileStream, "My Attachment.jpg");
            }
           
              myMessage.EnableClickTracking(false);
            // Create credentials, specifying your user name and password.
            var credentials = new NetworkCredential(username, passwd);

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            // You can also use the **DeliverAsync** method, which returns an awaitable task.
            transportWeb.Deliver(myMessage);
        }
    }
}
