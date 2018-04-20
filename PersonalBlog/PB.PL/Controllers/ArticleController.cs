using AutoMapper;
using PB.DAL;
using PB.DAL.Helpers;
using PB.DAL.Helpers.TableFields;
using PB.DAL.TableModels;
using PB.PL.Models;
using PB.PL.Models.Article;
using PB.PL.Models.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PB.PL.Controllers
{
    public class ArticleController : Controller
    {
        private PbRepository repos;

        public ArticleController()
        {
            this.repos = new PbRepository();
        }

        public ActionResult Index(int id)
        {
            if(ModelState.IsValid)
            {
                var model = Mapper.Map<ArticleContentVM>(this.repos.GetArticles(id));
                return View("Index", model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Authorize]
        public ActionResult CreateArticle(int blogId)
        {
            return View("CreateArticle", "Article", new CreateArticleVM() { BlogID = blogId });
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult CreateArticle(CreateArticleVM model)
        {
            if (ModelState.IsValid)
            {
                this.repos.CreateArticle(model.BlogID, model.ThemeID, model.Title, model.Content, model.Tags);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult Edit(int artId)
        {
            if (ModelState.IsValid)
            {
                var model = this.repos.GetArticles(artId);
                return View("Edit", "Article", new EditArticleVM() { ArticleID = artId, Content = model.Content, ThemeID = model.ThemeID, Title = model.Title });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Authorize]
        public ActionResult Edit(EditArticleVM model)
        {
            if (ModelState.IsValid)
            {
                var dict = new Dictionary<string, string>();
                dict.Add(ArticlesFields.ThemeID,Convert.ToString(model.ThemeID));
                dict.Add(ArticlesFields.Title, Convert.ToString(model.Title));
                dict.Add(ArticlesFields.Content, Convert.ToString(model.Content));
                this.repos.UpdateArticles(dict, model.ArticleID);
                return View("Index", "Article", model.ArticleID );
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet,Authorize]
        public PartialViewResult Comment(CreateCommentVM model)
        {
            return PartialView("CreateComment", model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult CreateComment(CreateCommentVM model)
        {
            if (ModelState.IsValid)
            {
                this.repos.CreateComments(model.ArticleID, this.repos.GetUserId(User.Identity.Name), model.Content, model.Parent);
            }
            return RedirectToAction("Index", "Article", new { id = model.ArticleID });

        }

        [HttpGet, Authorize]
        public PartialViewResult DeleteComment(int comId, int artId)
        {
            ViewData["artId"] = artId;
            return PartialView("ConfirmDeleteComment", comId);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult ConfirmDeleteComment(int comId,int artId)
        {
            if (ModelState.IsValid)
            {
                this.repos.DeleteComments(comId);
            }

            return RedirectToAction("Index", "Article", new { id = artId });

        }

        [ChildActionOnly]
        public PartialViewResult Comments(int id)
        {
            List<CommentListVM> model = new List<CommentListVM>();
            var queryResult = this.repos.GetComments(id);
            if(ModelState.IsValid && queryResult!=null)
            {
                foreach(var comment in queryResult)
                {
                    CommentListVM current = new CommentListVM();
                    current.Messages = new CommentsTree();
                    current.Messages.CommentID = comment.CommentID;
                    current.Messages.ArticleID = comment.ArticleID;
                    current.Messages.Content = comment.Content;
                    current.Messages.CreateDate = comment.CreateDate;
                    current.Messages.UserID = comment.UserID;
                    current.Messages.Parent = comment.Parent;
                    current.Messages.IdDeleted = comment.IdDeleted;

                    if (comment.ChildNodes != null)
                    {
                        current.Messages.ChildNodes = new List<DAL.TableModels.Comments>();
                        foreach (var child in comment.ChildNodes)
                        {
                            current.Messages.ChildNodes.Add(child);
                        }
                    }

                    model.Add(current);
                }
                return PartialView("Comments", model);
            }

            return null;
        }


        [ChildActionOnly]
        public PartialViewResult Tags(int id)
        {
            Dictionary<int, string> queryResult = this.repos.GetArticleTags(id);
            List<TagListVM> model = new List<TagListVM>();
            foreach (var item in queryResult)
            {
                model.Add(Mapper.Map<TagListVM>(item));
            }

            return PartialView("Tags", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Theme id from article VM model</param>
        /// <returns></returns>
        [ChildActionOnly]
        public string Theme(int id)
        {
            return this.repos.GetTheme(id);
        }

        /// <summary>
        /// need fix, replace by SelectListItem for support redirect to user page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public string Commentator(int id)
        {
            KeyValuePair<int,string> queryResult = this.repos.GetCommentator(id).First();
            return queryResult.Value;
            // new SelectListItem() { Value = Convert.ToString(queryResult.Key), Text = queryResult.Value };
        }
    }
}