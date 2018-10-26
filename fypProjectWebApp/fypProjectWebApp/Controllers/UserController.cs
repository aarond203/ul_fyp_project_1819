using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using fypProjectWebApp.Models;
using System.Web.Security;

namespace fypProjectWebApp.Controllers
{
    public class UserController : Controller
    {
        // Registration action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        // Registration POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "verify_email, activation_code")] User user)
        {
            bool status = false;
            string message = "";
            
            // Model validation
            if (ModelState.IsValid)
            {
                #region Email already exists
                var doesExist = DoesEmailExist(user.email_ID);
                if (doesExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exists");
                    return View(user);
                }
                #endregion

                #region  Generate code
                user.activation_code = Guid.NewGuid();
                #endregion

                #region Password Hashing
                user.user_pass = Encrypt.Hash(user.user_pass);
                user.confirm_pass = Encrypt.Hash(user.confirm_pass);
                #endregion

                user.verify_email = false;

                #region Save new user data to DB
                using (TestDatabase db = new TestDatabase())
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    // Send verification email
                    SendVerifyEmailLink(user.email_ID, user.activation_code.ToString());
                    message = "Registration complete. A verification email has been sent to " + user.email_ID;
                    status = true;
                }
                #endregion
            }
            else
            {
                message = "Invalid request";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
        }

        // Email Verification
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool status = false;

            using (TestDatabase db = new TestDatabase())
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var v = db.Users.Where(a => a.activation_code == new Guid(id)).FirstOrDefault();

                if (v != null)
                {
                    v.verify_email = true;
                    db.SaveChanges();
                    status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = status;
            return View();
        }

        // Login action
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Login POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl)
        {
            string message = "";

            using (TestDatabase db = new TestDatabase())
            {
                var v = db.Users.Where(a => a.email_ID == login.email_ID).FirstOrDefault();

                if (v != null)
                {
                    if (string.Compare(Encrypt.Hash(login.user_pass), v.user_pass) == 0)
                    {
                        int timeout = login.remember_me ? 525600 : 1;
                        var ticket = new FormsAuthenticationTicket(login.email_ID, login.remember_me, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);

                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    message = "Invalid credentials provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        // Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool DoesEmailExist(string email_ID)
        {
            using (TestDatabase db = new TestDatabase())
            {
                var v = db.Users.Where(a => a.email_ID == email_ID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerifyEmailLink(string email_ID, string activation_code)
        {
            var vUrl = "/User/VerifyAccount/" + activation_code;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, vUrl);

            var from = new MailAddress("aarond203test@gmail.com", "ULID Test");
            var to = new MailAddress(email_ID);
            var fromPass = "Kc@2U8IViL";

            var subject = "ULiD - Email Verification";
            string bodyMessage = 
                "Thank you for setting up your ULiD account.<br/><br/>Before you can proceed, we still need to make sure it's" +
                "you that has created the account. Please click on the link below to verify your email address:" +
                "<br/><a href='" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, fromPass)
            };

            using (var msg = new MailMessage(from, to)
            {
                Subject = subject,
                Body = bodyMessage,
                IsBodyHtml = true
            })

            smtp.Send(msg);
        }
    }
}