using AutoMapper;
using PB.DAL.Helpers;
using PB.DAL.TableModels;
using PB.PL.Models;
using PB.PL.Models.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PB.PL.App_Start
{
    public class AutoMapperConfig
    {

        public static void ConfigureMaps()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<KeyValuePair<int, string>, BlogListVM>()
                    .ForMember("BlogID", dest => dest.MapFrom(s => s.Key))
                    .ForMember("Title", dest => dest.MapFrom(s => s.Value));
                c.CreateMap<KeyValuePair<int, string>, ArticleListVM>()
                    .ForMember("ArticleID", dest => dest.MapFrom(s => s.Key))
                    .ForMember("Title", dest => dest.MapFrom(s => s.Value))
                    .ForMember(s => s.BlogID, dest => dest.Ignore());
                c.CreateMap<Articles, ArticleContentVM>()
                    .ForMember("ArticleID", dest => dest.MapFrom(s => s.ArticleID))
                    .ForMember("BlogID", dest => dest.MapFrom(s => s.BlogID))
                    .ForMember("ThemeID", dest => dest.MapFrom(s => s.ThemeID))
                    .ForMember("Title", dest => dest.MapFrom(s => s.Title))
                    .ForMember("Content", dest => dest.MapFrom(s => s.Content))
                    .ForMember("CreateDate", dest => dest.MapFrom(s => s.CreateDate))
                    .ForMember("UpdateDate", dest => dest.MapFrom(s => s.UpdateDate));
                c.CreateMap<Users, UserVM>()
                .ForMember(d => d.UserID, dest => dest.MapFrom(s => s.UserID))
                .ForMember(d => d.Login, dest => dest.MapFrom(s => s.Login))
                .ForMember(d => d.Email, dest => dest.MapFrom(s => s.Email))
                .ForMember(d => d.FirstName, dest => dest.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, dest => dest.MapFrom(s => s.LastName))
                .ForMember(d => d.Phone, dest => dest.MapFrom(s => s.Phone))
                .ForMember(d => d.RoleID, dest => dest.MapFrom(s => s.RoleID))
                .ForMember(d => d.RegistrationDate, dest => dest.MapFrom(s => s.RegistrationDate))
                .ForMember(d => d.BlogID, dest => dest.MapFrom(s => s.BlogID));
                c.CreateMap<Users, EditUserVM>()
               .ForMember(d => d.Login, dest => dest.MapFrom(s => s.Login))
               .ForMember(d => d.Email, dest => dest.MapFrom(s => s.Email))
               .ForMember(d => d.FirstName, dest => dest.MapFrom(s => s.FirstName))
               .ForMember(d => d.LastName, dest => dest.MapFrom(s => s.LastName))
               .ForMember(d => d.Phone, dest => dest.MapFrom(s => s.Phone));
                c.CreateMap<UserVM, EditUserVM>()
                .ForMember(d => d.Login, dest => dest.MapFrom(s => s.Login))
                .ForMember(d => d.Email, dest => dest.MapFrom(s => s.Email))
                .ForMember(d => d.FirstName, dest => dest.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, dest => dest.MapFrom(s => s.LastName))
                .ForMember(d => d.Phone, dest => dest.MapFrom(s => s.Phone));
                c.CreateMap<KeyValuePair<int, string>, TagListVM>()
                .ForMember("TagID", dest => dest.MapFrom(s => s.Key))
                .ForMember("TagName", dest => dest.MapFrom(s => s.Value));
                //c.CreateMap<CommentsTree, CommentListVM>()
                //    .ForMember(s=>s.Messages.CommentID, dest => dest.MapFrom(s => s.CommentID))
                //    .ForMember(s=>s.Messages.ArticleID, dest => dest.MapFrom(s => s.ArticleID))
                //    .ForMember(s => s.Messages.Content, dest => dest.MapFrom(s => s.Content))
                //    .ForMember(s => s.Messages.CreateDate, dest => dest.MapFrom(s => s.CreateDate))
                //    .ForMember(s => s.Messages.UserID, dest => dest.MapFrom(s => s.UserID))
                //    .ForMember(s => s.Messages.Parent, dest => dest.MapFrom(s => s.Parent))
                //    .ForMember(s => s.Messages.IdDeleted, dest => dest.MapFrom(s => s.IdDeleted))
                //    .ForMember(s=>s.Messages.ChildNodes, dest => dest.Ignore());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}