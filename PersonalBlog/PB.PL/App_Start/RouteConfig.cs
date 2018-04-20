using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PB.PL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "blogList",
                url: "{controller}/{action_id}",
                defaults: new { controller = "Blog", action = "Index"},
                constraints: new
                {
                    id = @"\d*"
                }
            );

            routes.MapRoute(
                name: "articleList",
                url: "{controller}/{action_id}",
                defaults: new { controller = "Article", action = "Index" },
                constraints: new
                {
                    id = @"\d*"
                }
            );

            routes.MapRoute(
                name: "createComment",
                url: "{controller}/{action_articleId_userId_content_parent}",
                defaults: new { controller = "Article", action = "CreateComment"},
                constraints: new
                {
                    articleId = @"\d*",
                    userId = @"\d*"
                }
            );


            routes.MapRoute(
                name: "deleteComment",
                url: "{controller}/{action_comId_artId}",
                defaults: new { controller = "Article", action = "DeleteComment"},
                constraints: new
                {
                    comId = @"\d*",
                    artId = @"\d*"
                }
            );

            routes.MapRoute(
                name: "confirmDeleteComment",
                url: "{controller}/{action_comId_artId}",
                defaults: new { controller = "Article", action = "ConfirmDeleteComment" },
                constraints: new
                {
                    comId = @"\d*",
                    artId = @"\d*"
                }
            );

            routes.MapRoute(
               name: "userRegister",
               url: "{controller}/{action_login_password_fname_lname_email_phone}",
               defaults: new { controller = "User", action = "Edit", login = UrlParameter.Optional, password = UrlParameter.Optional, fname = UrlParameter.Optional, lname = UrlParameter.Optional, email = UrlParameter.Optional, phone = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "logon",
                url: "{controller}/{action_login_password_passwordComfarmation}",
                defaults: new { controller = "User", action = "Logon", login = UrlParameter.Optional, password = UrlParameter.Optional, passwordComfarmation = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "logoff",
                url: "{controller}/{action}",
                defaults: new { controller = "User", action = "Logoff" }
            );

            routes.MapRoute(
               name: "user",
               url: "{controller}/{action}",
               defaults: new { controller = "User", action = "Index"}
           );

            routes.MapRoute(
               name: "userEdit",
               url: "{controller}/{action}",
               defaults: new { controller = "User", action = "Edit"}
           );

            routes.MapRoute(
              name: "myBlog",
              url: "{controller}/{action_login}",
              defaults: new { controller = "Blog", action = "MyBlog", login = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "createBlog",
              url: "{controller}/{action_Title_UserLogin}",
              defaults: new { controller = "Blog", action = "Create", Title = UrlParameter.Optional, UserLogin = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "editBlog",
              url: "{controller}/{action_BlogID_Title}",
              defaults: new { controller = "Blog", action = "Edit", BlogID = UrlParameter.Optional, Title = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "createArticle",
              url: "{controller}/{action_BlogID_ThemeID_Title_Content_Tags}",
              defaults: new { controller = "Article", action = "Create", BlogID = UrlParameter.Optional, ThemeID = UrlParameter.Optional, Title = UrlParameter.Optional, Content = UrlParameter.Optional, Tags = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "editArticle",
              url: "{controller}/{action_ArticleID_ThemeID_Title_Content}",
              defaults: new { controller = "Article", action = "Edit", ArticleID = UrlParameter.Optional, ThemeID = UrlParameter.Optional, Title = UrlParameter.Optional, Content = UrlParameter.Optional }
          );
        }
    }
}
