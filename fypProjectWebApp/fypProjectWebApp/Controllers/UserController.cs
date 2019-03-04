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
        public ActionResult Registration([Bind(Exclude = "verifyEmail, activationCode")] User user)
        {
            bool status = false;
            string message = "";
            
            // Model validation
            if (ModelState.IsValid)
            {
                #region Email already exists
                var doesExist = DoesEmailExist(user.emailID);
                if (doesExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exists");
                    return View(user);
                }
                #endregion

                #region  Generate code
                user.activationCode = Guid.NewGuid();
                #endregion

                #region Password Hashing
                user.userPass = Encrypt.Hash(user.userPass);
                user.confirmPass = Encrypt.Hash(user.confirmPass);
                #endregion

                user.verifyEmail = false;

                #region Save new user data to DB
                using (fypDatabase db = new fypDatabase())
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    // Send verification email
                    SendVerifyEmailLink(user.emailID, user.activationCode.ToString());
                    message = "Registration complete. A verification email has been sent to " + user.emailID;
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

            using (fypDatabase db = new fypDatabase())
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var v = db.Users.Where(a => a.activationCode == new Guid(id)).FirstOrDefault();

                if (v != null)
                {
                    v.verifyEmail = true;
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

            using (fypDatabase db = new fypDatabase())
            {
                var v = db.Users.Where(a => a.emailID == login.email_ID).FirstOrDefault();

                if (v != null)
                {
                    if (string.Compare(Encrypt.Hash(login.user_pass), v.userPass) == 0)
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
            using (fypDatabase db = new fypDatabase())
            {
                var v = db.Users.Where(a => a.emailID == email_ID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerifyEmailLink(string emailID, string activationCode)
        {
            var vUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, vUrl);

            var from = new MailAddress("aarond203test@gmail.com", "UL Pay Test");
            var to = new MailAddress(emailID);
            var fromPass = "Kc@2U8IViL";

            var subject = "UL Pay - Email Verification";
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