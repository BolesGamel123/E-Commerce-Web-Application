using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class ContactModelController : Controller
    {
        [HttpGet]
        public ActionResult ContactMessage()
        {


            return View();
        
        }





        [HttpPost]
            public ActionResult ContactMessage(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginfo = new NetworkCredential("mariohemaya43@gmail.com","ManoMan2020");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("mariohemaya43@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "The sender's name:" + contact.Name + "<br/>" +
                         "The sender's email:" + contact.Email + "<br/>" +
                         " Title of message:" + contact.Subject + "<br/>" +
                         "body of message:" + contact.Message;

            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginfo;
            
            smtpClient.Send(mail);
            return RedirectToAction("Index", "Home");
        }
    }
}