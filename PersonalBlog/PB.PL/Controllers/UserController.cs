using AutoMapper;
using PB.DAL;
using PB.DAL.Helpers.TableFields;
using PB.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PB.PL.Controllers
{
    public class UserController : Controller
    {
        private PbRepository repos;

        public UserController()
        {
            repos = new PbRepository();
        }

        [HttpGet, Authorize]
        public ActionResult Index()
        {
            if (User.Identity != null && !string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                var user = Mapper.Map<UserVM>(repos.GetUser(User.Identity.Name));
                return View("Index", user);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationFormVM model)
        {
            var ch = repos.CreateUser(model.Login, model.Password, model.FirstName, model.LastName, model.Email, model.Phone);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult Logon()
        {
            return View("Logon");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logon(LogonVM model)
        {
            var ch = repos.CheckUserLoginPasswordPair(model.Login, model.Password);
            if(ch == 0)
            {
                FormsAuthentication.RedirectFromLoginPage(model.Login, createPersistentCookie: true);
                return RedirectToAction("Index", "User", Mapper.Map<UserVM>(repos.GetUser(model.Login)));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet,Authorize]
        public ActionResult Edit()
        {
            var model = this.repos.GetUser(User.Identity.Name);
            return View("Edit","User",Mapper.Map<EditUserVM>(model));
        }

        [HttpPost, ValidateAntiForgeryToken,Authorize]
        public ActionResult Edit(EditUserVM model)
        {
            if (ModelState.IsValid)
            {
                var id = repos.GetUserId(model.Login);
                Dictionary<string, string> dict = new Dictionary<string, string>
            {
                { UsersFields.Login, model.Login },
                { UsersFields.Login, model.Email },
                { UsersFields.Login, model.FirstName },
                { UsersFields.Login, model.LastName },
                { UsersFields.Login, model.Phone }
            };
                repos.UpdateUser(dict,id);
            }

            return RedirectToAction("Index", "User", Mapper.Map<UserVM>(repos.GetUser(model.Login)));
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public bool IsAdmin(string login)
        {
            return repos.IsAdmin(login);
        }

        [ChildActionOnly]
        public string GetLogin(int? userId)
        {
            return (userId.HasValue) ? repos.GetUserLogin(userId.Value) : User.Identity.Name;
        }

        [ChildActionOnly]
        public string HaveBlog(string login)
        {
            return (!repos.UserNotAlreadyHaveBlog(repos.GetUserId(login))) ? "true" : "false";
        }

        [ChildActionOnly]
        public string IsUserBlog(int? blogId, string login)
        {
            if (blogId.HasValue)
            {
                return (repos.IsUserBlog(blogId.Value, login)) ? "true" : "false";
            }

            return "false";
        }

        [ChildActionOnly]
        public string IsUserArticle(int? artId,string login)
        {
            if (artId.HasValue)
            {
                return (repos.IsUserArticle(artId.Value, login)) ? "true" : "false";
            }

            return "false";
        }
    }
}