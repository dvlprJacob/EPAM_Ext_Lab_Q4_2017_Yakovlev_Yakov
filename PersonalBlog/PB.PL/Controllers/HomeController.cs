using AutoMapper;
using PB.DAL;
using PB.PL.Models.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PB.PL.Controllers
{
    public class HomeController : Controller
    {
        private PbRepository repos;

        public HomeController()
        {
            this.repos = new PbRepository();
        }

        public ActionResult Index()
        {
            Dictionary<int, string> queryResult = this.repos.GetAllBlogs();
            List<BlogListVM> model = new List<BlogListVM>();
            foreach(var item in queryResult)
            {
                model.Add(Mapper.Map<BlogListVM>(item));
            }

            return View("Index", model);
        }

        [ChildActionOnly]
        public PartialViewResult PopularArticles()
        {
            Dictionary<int, string> queryResult = this.repos.GetPopularArticles();
            List<ArticleListVM> model = new List<ArticleListVM>();
            foreach (var item in queryResult)
            {
                model.Add(Mapper.Map<ArticleListVM>(item));
            }

            return PartialView("PopularArticles",model);
        }

        [ChildActionOnly]
        public PartialViewResult Tags()
        {
            Dictionary<int, string> queryResult = this.repos.GetAllTags();
            List<TagListVM> model = new List<TagListVM>();
            foreach (var item in queryResult)
            {
                model.Add(Mapper.Map<TagListVM>(item));
            }

            return PartialView("Tags", model);
        }
    }
}