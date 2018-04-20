using AutoMapper;
using PB.DAL;
using PB.DAL.Helpers.TableFields;
using PB.PL.Models;
using PB.PL.Models.Blog;
using PB.PL.Models.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PB.PL.Controllers
{
    public class BlogController : Controller
    {
        private PbRepository repos;
        
        public BlogController()
        {
            this.repos = new PbRepository();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> Blog id</param>
        /// <returns> Blog articles list </returns>
        public ActionResult Index(int id)
        {

            if (ModelState.IsValid)
            {
                Dictionary<int, string> queryResult = this.repos.GetAllArticles(id);
                List<ArticleListVM> model = new List<ArticleListVM>();
                foreach (var item in queryResult)
                {
                    model.Add(Mapper.Map<ArticleListVM>(item));
                    model.Last().BlogID = this.repos.GetBlogId(item.Key);
                }

                return View("Index", model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Authorize]
        public ActionResult MyBlog(string login)
        {
            if (ModelState.IsValid)
            {
                Dictionary<int, string> queryResult = this.repos.GetAllArticles(login);
                List<ArticleListVM> model = new List<ArticleListVM>();
                foreach (var item in queryResult)
                {
                    model.Add(Mapper.Map<ArticleListVM>(item));
                    model.Last().BlogID = this.repos.GetBlogId(login);
                }

                return View("Index", model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Authorize]
        public ActionResult Create()
        {
            return View("Create","Blog", new CreateBlogVM());
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult Create(CreateBlogVM model)
        {
            if (ModelState.IsValid)
            {
                this.repos.CreateBlog(this.repos.GetUserId(model.UserLogin), model.Title);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Authorize]
        public ActionResult Edit()
        {
            var model = this.repos.GetBlog(this.repos.GetBlogId(User.Identity.Name));
            return View("CreateBlog", "Blog",new EditBlogVM() { BlogID = model.BlogID, Title = model.Title });
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult Edit(EditBlogVM model)
        {
            if (ModelState.IsValid)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>
                {
                    { BlogsFields.Title, model.Title }
                };

                return (this.repos.UpdateBlogs(dict, model.BlogID)) ? RedirectToAction("Index","Blog",model.BlogID) : RedirectToAction("Index", "Home");
            }

            return null;
        }
    }
}