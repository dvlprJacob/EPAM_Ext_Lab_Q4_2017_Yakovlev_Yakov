using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using PB.DAL.TableModels;
using PB.DAL.Helpers.TableFields;
using PB.DAL.Helpers;

namespace PB.DAL
{
    public class PbRepository
    {
        #region private members

        private string connectionString;

        private DbProviderFactory providerFactory;

        /// <summary>
        /// Table field updated value methods refs, key - field name, value - func with input params : record id, new value
        /// </summary>
        private Dictionary<string, Func<int, string, bool>> updateUsersField;
        private Dictionary<string, Func<int, string, bool>> updateBlogsField;
        private Dictionary<string, Func<int, string, bool>> updateArticlesField;

        #endregion

        public PbRepository()
        {
            try
            {
                this.UnresolvedExceptions = new List<Exception>();
                var conStringTemp = ConfigurationManager.ConnectionStrings["PersonalBlogConnection"];
                this.connectionString = conStringTemp.ConnectionString;
                this.providerFactory = DbProviderFactories.GetFactory(conStringTemp.ProviderName);
                this.AddUpdateMethodsRefs();
            }

            catch(Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
            }
        }

        public List<Exception> UnresolvedExceptions
        {
            get;
            set;
        }

        /// User can have only one active blog, links by FK, article links to blog by FK, blog can have many articles, tags links to article by links table,
        /// article can have more comments, also comment trees
        #region CRUD methods for Blog parts

        #region Blogs table

        /// <summary>
        /// Create blog with title for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <returns>true, for successful creating blogs record and update users record, else false</returns>
        public bool CreateBlog(int userId, string title)
        {
            try
            {
                if (this.UserNotAlreadyExist(userId))
                {
                    throw new ArgumentException($"User with id = {userId} already does not exist");
                }

                if (!this.UserNotAlreadyHaveBlog(userId))
                {
                    throw new ArgumentException($"User with id = {userId} already have blog");
                }

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand crBlogComm = (SqlCommand)currConnection.CreateCommand();
                    crBlogComm.CommandText = "INSERT INTO [dbo].[Blogs] ([Title], [IsDeleted], [CreatorID]) VALUES (@title, 0, NULL)";
                    crBlogComm.Parameters.AddWithValue("@title", title);
                    if (crBlogComm.ExecuteNonQuery() == 0)
                    {
                        throw new Exception($"Failed to create blog with title = {title} for user with id = {userId}");
                    }

                    SqlCommand selCurBlogIdComm = (SqlCommand)currConnection.CreateCommand();
                    selCurBlogIdComm.CommandText = "SELECT IDENT_CURRENT('[dbo].[Blogs]')";

                    SqlCommand updUserCom = (SqlCommand)currConnection.CreateCommand();
                    updUserCom.CommandText = "UPDATE [dbo].[Users] SET BlogID = @blogId WHERE UserID = @userId";
                    updUserCom.Parameters.AddWithValue("@blogId", (int)selCurBlogIdComm.ExecuteScalar());
                    updUserCom.Parameters.AddWithValue("@userId", userId);

                    if (updUserCom.ExecuteNonQuery() == 0)
                    {
                        throw new Exception($"Failed to update user BlogId with new value = {(int)selCurBlogIdComm.ExecuteScalar()} for user with id = {userId}");
                    }

                    return true;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        public Blogs GetBlog(int blogId)
        {
            try
            {
                Blogs result = new Blogs();

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selBlogComm = (SqlCommand)currConnection.CreateCommand();
                    selBlogComm.CommandText = "SELECT * FROM [dbo].[Blogs] WHERE BlogID = @blogId AND IsDeleted = 0";
                    selBlogComm.Parameters.AddWithValue("@blogId", blogId);

                    using (SqlDataReader reader = selBlogComm.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            throw new Exception($"Blog with id = {blogId} is not exist");
                        }

                        reader.Read();
                        result.BlogID = (int)reader[BlogsFields.BlogID];
                        result.Title = reader[BlogsFields.Title] as string;
                    }
                    
                    return result;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public int GetBlogId(string login)
        {
            try
            {

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selBlogComm = (SqlCommand)currConnection.CreateCommand();
                    selBlogComm.CommandText = "SELECT BlogID FROM [dbo].[Users] WHERE Login = @login AND IsDeleted = 0";
                    selBlogComm.Parameters.AddWithValue("@login", login);

                    return (int)selBlogComm.ExecuteScalar();

                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return -1;
            }
        }

        public int GetBlogId(int artId)
        {
            try
            {

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selBlogComm = (SqlCommand)currConnection.CreateCommand();
                    selBlogComm.CommandText = "SELECT BlogID FROM [dbo].[Articles] WHERE ArticleID = @artId AND IsDeleted = 0";
                    selBlogComm.Parameters.AddWithValue("@artId", artId);

                    return (int)selBlogComm.ExecuteScalar();

                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return -1;
            }
        }

        public Dictionary<int, string> GetAllBlogs()
        {
            try
            {
                Dictionary<int, string> result = new Dictionary<int, string>();

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selBlogsComm = (SqlCommand)currConnection.CreateCommand();
                    selBlogsComm.CommandText = "SELECT BlogID, Title FROM [dbo].[Blogs] WHERE IsDeleted = 0";

                    using (IDataReader reader = selBlogsComm.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            result.Add((int)reader[BlogsFields.BlogID], reader[BlogsFields.Title] as string);
                        }
                    }

                    return result;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public bool UpdateBlogs(Dictionary<string, string> newValues, int blogId)
        {
            try
            {
                if (!BlogsFields.IsArticlesTableFields(newValues.Keys))
                {
                    throw new ArgumentException("Input field name is not contain Blogs table fields");
                }

                foreach (var newVal in newValues)
                {
                    this.updateBlogsField.First(kvp => kvp.Key == newVal.Key).Value.Invoke(blogId, newVal.Value);
                }

                return true;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        private bool UpdateBlogTitle(int blogId, string newValue)
        {
            try
            {
                if (newValue == string.Empty)
                {
                    this.UnresolvedExceptions.Add(new ArgumentException("Blog title can not empty"));
                    return false;
                }

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Blogs] SET Title = @newValue  WHERE BlogID = @blogId";
                    selectUsersComm.Parameters.AddWithValue("@newValue", newValue);
                    selectUsersComm.Parameters.AddWithValue("@blogId", blogId);
                    int chQueryRes = selectUsersComm.ExecuteNonQuery();
                    if (chQueryRes == 0)
                    {
                        this.UnresolvedExceptions.Add(new ArgumentException($"Record in Blogs table with id = {blogId} is not exist"));
                    }

                    return (chQueryRes == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Set Users record for BlogID field NULL value, IsDeleted field in Blogs table replace by true, i.e delete user blog
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <returns></returns>
        public bool DeleteBlog(int userId)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selBlComm = (SqlCommand)currConnection.CreateCommand();
                    selBlComm.CommandText = "SELECT BlogID FROM [dbo].[Users] WHERE UserID = @userId";
                    selBlComm.Parameters.AddWithValue("@userId", userId);

                    int? blogId = selBlComm.ExecuteScalar() is DBNull ? null : (int?)selBlComm.ExecuteScalar();
                    if (!blogId.HasValue)
                    {
                        this.UnresolvedExceptions.Add(new ArgumentException($"User with id = {userId} doesn't have blog"));
                        return false;
                    }

                    SqlCommand delComm = (SqlCommand)currConnection.CreateCommand();
                    delComm.CommandText = "UPDATE [dbo].[Users] SET BlogID = NULL WHERE UserID = @userId";
                    delComm.Parameters.AddWithValue("@userId", userId);

                    int chQueryRes = delComm.ExecuteNonQuery();
                    if (chQueryRes == 0)
                    {
                        return false;
                    }

                    SqlCommand updComm = (SqlCommand)currConnection.CreateCommand();
                    updComm.CommandText = "UPDATE [dbo].[Blogs] SET IsDeleted = 1, CreatorID = @userId WHERE BlogID = @blogId";
                    updComm.Parameters.AddWithValue("@userId", userId);
                    updComm.Parameters.AddWithValue("@blogId", blogId.Value);

                    return (updComm.ExecuteNonQuery() == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        #endregion


        #region Articles table

        public bool CreateArticle(int blogId, int themeId, string title, string content, List<string> tags)
        {
            try
            {
                if (title == string.Empty || content == string.Empty)
                {
                    return false;
                }

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand crArtComm = (SqlCommand)currConnection.CreateCommand();
                    crArtComm.CommandText = "INSERT INTO [dbo].[Articles] ([BlogID], [ThemeID], [Title], [CreateDate], [UpdateDate], [Content], [IsDeleted]) "
                        + "VALUES (@blogId, @themeId, @title, @crDate, @updDate, @content, @isDel)";

                    crArtComm.Parameters.AddWithValue("@blogId", blogId);
                    crArtComm.Parameters.AddWithValue("@isDel", 0);
                    crArtComm.Parameters.AddWithValue("@crDate", DateTime.Now.ToShortDateString());
                    crArtComm.Parameters.AddWithValue("@themeId", themeId);
                    crArtComm.Parameters.AddWithValue("@title", title);
                    crArtComm.Parameters.AddWithValue("@updDate", DBNull.Value);
                    crArtComm.Parameters.AddWithValue("@content", content);

                    return (crArtComm.ExecuteNonQuery() == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        public Articles GetArticles(int artId)
        {
            Articles result = new Articles();
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectArtComm = (SqlCommand)currConnection.CreateCommand();

                    selectArtComm.CommandText = "SELECT * FROM [dbo].[Articles] WHERE IsDeleted = 0 AND ArticleID = @artId";
                    selectArtComm.Parameters.AddWithValue("@artId", artId);

                    using (IDataReader reader = selectArtComm.ExecuteReader())
                    {
                        reader.Read();

                        result.ArticleID = (int)reader[ArticlesFields.ArticleID];
                        result.BlogID = (int)reader[ArticlesFields.BlogID];
                        result.ThemeID = (int)reader[ArticlesFields.ArticleID];
                        result.Title = reader[ArticlesFields.Title] as string;
                        result.CreateDate = (DateTime)reader[ArticlesFields.CreateDate];
                        result.UpdateDate = reader[ArticlesFields.UpdateDate] as DateTime?;
                        result.Content = reader[ArticlesFields.Content] as string;
                        result.IsDeleted = (bool)reader[ArticlesFields.IsDeleted];
                    }
                }

                return result;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        /// <summary>
        /// Used for view blog artcles
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns> Return blog availability articles by blogId, key - artId, value - title</returns>
        public Dictionary<int,string> GetAllArticles(int blogId)
        {
            Dictionary<int, string> resultEnum = new Dictionary<int, string>();
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectArtComm = (SqlCommand)currConnection.CreateCommand();
                    
                    selectArtComm.CommandText = "SELECT ArticleID, Title FROM [dbo].[Articles] WHERE IsDeleted = 0 AND BlogID = @blogId ORDER BY CreateDate DESC";
                    selectArtComm.Parameters.AddWithValue("@blogId", blogId);

                    using (IDataReader reader = selectArtComm.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            resultEnum.Add((int)reader[ArticlesFields.ArticleID], reader[ArticlesFields.Title] as string);
                        }
                    }
                }

                return resultEnum;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public Dictionary<int, string> GetAllArticles(string login)
        {
            Dictionary<int, string> resultEnum = new Dictionary<int, string>();
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectArtComm = (SqlCommand)currConnection.CreateCommand();

                    selectArtComm.CommandText = "SELECT ArticleID, Title FROM [dbo].[Articles] WHERE IsDeleted = 0 AND BlogID = (SELECT BlogID FROM [dbo].[Users] WHERE Login = @login) ORDER BY CreateDate DESC";
                    selectArtComm.Parameters.AddWithValue("@login", login);

                    using (IDataReader reader = selectArtComm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultEnum.Add((int)reader[ArticlesFields.ArticleID], reader[ArticlesFields.Title] as string);
                        }
                    }
                }

                return resultEnum;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        /// <summary>
        /// Popular by comments
        /// </summary>
        /// <param name="topCount"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetPopularArticles(int topCount = 10)
        {
            Dictionary<int, string> resultEnum = new Dictionary<int, string>();
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectArtComm = (SqlCommand)currConnection.CreateCommand();

                    selectArtComm.CommandText = "SELECT TOP (@topCount) ArticleID, (SELECT Title FROM Articles a WHERE a.ArticleID = c.ArticleID) AS 'Title' FROM Comments c GROUP BY c.ArticleID ORDER BY COUNT(c.ArticleID) DESC";
                    selectArtComm.Parameters.AddWithValue("@topCount", topCount);

                    using (IDataReader reader = selectArtComm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultEnum.Add((int)reader[ArticlesFields.ArticleID], reader[ArticlesFields.Title] as string);
                        }
                    }
                }

                return resultEnum;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        /// <summary>
        /// Used for search articles by theme
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns>key - artId, value - title</returns>
        public Dictionary<int, string> FindArticlesByTheme(int themeId)
        {
            Dictionary<int, string> resultEnum = new Dictionary<int, string>();

            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectArtComm = (SqlCommand)currConnection.CreateCommand();

                    selectArtComm.CommandText = "SELECT ArticleID, Title FROM [dbo].[Articles] WHERE IsDeleted = 0 AND ThemeID = @themeId ORDER BY CreateDate DESC";
                    selectArtComm.Parameters.AddWithValue("@themeId", themeId);

                    using (IDataReader reader = selectArtComm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultEnum.Add((int)reader[ArticlesFields.ArticleID], reader[ArticlesFields.Title] as string);
                        }
                    }
                }

                return resultEnum;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public Dictionary<int, string> FindArticlesByTags(List<int> tagsId)
        {
            Dictionary<int, string> resultEnum = new Dictionary<int, string>();

            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    /// Get all related article identifiers

                    SqlCommand selectArtRefsComm = (SqlCommand)currConnection.CreateCommand();

                    var commandTextR = "SELECT ArticleID FROM [dbo].[TagLinks] WHERE TagID in ( ";

                    foreach(var id in tagsId)
                    {
                        string curIdParam = "@" + Convert.ToString(id);
                        commandTextR += curIdParam + ", ";
                        selectArtRefsComm.Parameters.AddWithValue(curIdParam, id);
                    }

                    selectArtRefsComm.CommandText = commandTextR.Remove(commandTextR.Length - 2, 2) + ") AND IsDeleted = 0";

                    List<int> artRefs = new List<int>(); 
                    using (IDataReader reader = selectArtRefsComm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            artRefs.Add((int)reader[TagLinksFields.ArticleID]);
                        }
                    }

                    /// Will build result

                    SqlCommand selectArtsComm = (SqlCommand)currConnection.CreateCommand();

                    var commandTextA = "SELECT * FROM [dbo].[Articles] WHERE ArticleID in ( ";

                    foreach (var id in artRefs)
                    {
                        string curIdParam = "@" + Convert.ToString(id);
                        commandTextA += curIdParam + ", ";
                        selectArtsComm.Parameters.AddWithValue(curIdParam, id);
                    }

                    selectArtsComm.CommandText = commandTextA.Remove(commandTextA.Length - 2, 2) + ")";

                    using (IDataReader reader = selectArtsComm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultEnum.Add((int)reader[ArticlesFields.ArticleID], reader[ArticlesFields.Title] as string);
                        }
                    }
                }

                return resultEnum;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public bool UpdateArticles(Dictionary<string, string> newValues, int artId)
        {
            try
            {
                if (!ArticlesFields.IsArticlesTableFields(newValues.Keys))
                {
                    throw new ArgumentException("Input field name is not contain Articles table fields");
                }

                foreach (var newVal in newValues)
                {
                    this.updateArticlesField .First(kvp => kvp.Key == newVal.Key).Value.Invoke(artId, newVal.Value);
                }

                return true;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        private bool UpdateArticlesTitle(int artId, string newValue)
        {
            try
            {
                if (newValue == string.Empty)
                {
                    this.UnresolvedExceptions.Add(new ArgumentException($"Article title not can empty"));
                    return false;
                }
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Articles] SET Title = @title, UpdateDate = @updDate WHERE ArticleID = @artId";
                    selectUsersComm.Parameters.AddWithValue("@artId", artId);
                    selectUsersComm.Parameters.AddWithValue("@title", newValue);
                    selectUsersComm.Parameters.AddWithValue("@updDate", DateTime.Now.ToShortDateString());

                    return (selectUsersComm.ExecuteNonQuery() == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        private bool UpdateArticlesContent(int artId, string newValue)
        {
            try
            {
                if(newValue == string.Empty)
                {
                    this.UnresolvedExceptions.Add(new ArgumentException($"Article content not can clear"));
                    return false;
                }
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Articles] SET Content = @content, UpdateDate = @updDate  WHERE ArticleID = @artId";
                    selectUsersComm.Parameters.AddWithValue("@artId", artId);
                    selectUsersComm.Parameters.AddWithValue("@content", newValue);
                    selectUsersComm.Parameters.AddWithValue("@updDate", DateTime.Now.ToShortDateString());

                    return (selectUsersComm.ExecuteNonQuery() == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        public bool DeleteArticle(int artId)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();
                    SqlCommand selBlComm = (SqlCommand)currConnection.CreateCommand();
                    selBlComm.CommandText = "UPDATE [dbo].[Users] SET IsDeleted = 1 WHERE ArticleID = @artId";
                    selBlComm.Parameters.AddWithValue("@artId", artId);

                    return (selBlComm.ExecuteNonQuery() == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        #endregion


        #region Comments table

        public bool CreateComments(int artId, int userId, string content, int? parent)
        {
            try
            {
                if (content == string.Empty)
                {
                    this.UnresolvedExceptions.Add(new ArgumentException($"Comment content not can empty"));
                    return false;
                }

                using (var currConnection = this.providerFactory.CreateConnection())
                {
                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand comm = (SqlCommand)currConnection.CreateCommand();
                    comm.CommandText = "INSERT INTO [dbo].[Comments] ([ArticleID], [UserID], [Content], [CreateDate], [Parent], [IsDeleted]) "
                        + "VALUES (@artId, @userId, @content, @crDate, @parent, 0)";
                    comm.Parameters.AddWithValue("@artId", artId);
                    comm.Parameters.AddWithValue("@userId", userId);
                    comm.Parameters.AddWithValue("@content", content);
                    comm.Parameters.AddWithValue("@crDate", DateTime.Now.ToShortDateString());
                    if (parent.HasValue)
                    {
                        comm.Parameters.AddWithValue("@parent", parent);
                    }
                    else
                    {
                        comm.Parameters.AddWithValue("@parent", DBNull.Value);
                    }

                    return (comm.ExecuteNonQuery() == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// For start comments can have one level of nesting,
        /// in fact, the database architecture allows you to do more
        /// </summary>
        /// <param name="artId"> article id</param>
        /// <returns> article comments tree</returns>
        public List<CommentsTree> GetComments(int artId)
        {
            List<CommentsTree> resultEnum = new List<CommentsTree>();

            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selRootsCommand = (SqlCommand)currConnection.CreateCommand();

                    selRootsCommand.CommandText = "SELECT * FROM [dbo].[Comments] WHERE IsDeleted = 0 AND ArticleID = @artId AND Parent IS NULL ORDER BY CreateDate DESC";
                    selRootsCommand.Parameters.AddWithValue("@artId", artId);

                    /// Block read first level comments
                    using (IDataReader reader = selRootsCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CommentsTree root = new CommentsTree();
                            root.CommentID = (int)reader[CommentsFields.CommentID];
                            root.ArticleID = (int)reader[CommentsFields.ArticleID];
                            root.UserID = (int)reader[CommentsFields.UserID];
                            root.Content = reader[CommentsFields.Content] as string;
                            root.CreateDate = Convert.ToDateTime(reader[CommentsFields.CreateDate]);
                            root.Parent = reader[CommentsFields.Parent] as int?;
                            root.IdDeleted = (bool)reader[CommentsFields.IsDeleted];

                            resultEnum.Add(root);
                        }
                    }

                    foreach(var com in resultEnum)
                    {
                        SqlCommand selChildsCommand = (SqlCommand)currConnection.CreateCommand();

                        selChildsCommand.CommandText = "SELECT * FROM [dbo].[Comments] WHERE ArticleID = @artId AND IsDeleted = 0  AND Parent = @parent";
                        selChildsCommand.Parameters.AddWithValue("@artID", artId);
                        selChildsCommand.Parameters.AddWithValue("@parent", com.CommentID);

                        /// Block read second level comments, i.e current root comment childs
                        using (IDataReader readerCh = selChildsCommand.ExecuteReader())
                        {
                            com.ChildNodes = new List<Comments>();
                            while (readerCh.Read())
                            {
                                Comments curChild = new Comments();
                                curChild.CommentID = (int)readerCh[CommentsFields.CommentID];
                                curChild.ArticleID = (int)readerCh[CommentsFields.ArticleID];
                                curChild.UserID = (int)readerCh[CommentsFields.UserID];
                                curChild.Content = readerCh[CommentsFields.Content] as string;
                                curChild.CreateDate = Convert.ToDateTime(readerCh[CommentsFields.CreateDate]);
                                curChild.Parent = readerCh[CommentsFields.Parent] as int?;
                                curChild.IdDeleted = (bool)readerCh[CommentsFields.IsDeleted];

                                com.ChildNodes.Add(curChild);
                            }
                        }
                    }
                }
                return resultEnum;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public bool DeleteComments(int commentId)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand comm = (SqlCommand)currConnection.CreateCommand();
                    comm.CommandText = "UPDATE [dbo].[Comments] SET IsDeleted = 1 WHERE CommentID = @comId";
                    comm.Parameters.AddWithValue("@comId", commentId);

                    return (comm.ExecuteNonQuery() == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        public Dictionary<int, string> GetCommentator(int userId)
        {
            try
            {
                Dictionary<int, string> resultUser = new Dictionary<int, string>();

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selUserComm = (SqlCommand)currConnection.CreateCommand();
                    selUserComm.CommandText = "SELECT UserID, FirstName, LastName FROM [dbo].[Users] WHERE IsDeleted = 0 AND UserID = @userId";
                    selUserComm.Parameters.AddWithValue("@userId",userId);

                    using (SqlDataReader reader = selUserComm.ExecuteReader())
                    {
                        reader.Read();

                        return new Dictionary<int, string>() {
                            { (int)reader[UsersFields.UserID], string.Concat(reader[UsersFields.FirstName] as string," ",reader[UsersFields.LastName] as string) } };
                    }
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        #endregion


        #region Tags, TagLinks tables


        public bool CreateTags(List<string> tags, int artId)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand insCommand = (SqlCommand)currConnection.CreateCommand();
                    insCommand.Parameters.AddWithValue("@artId", artId);

                    var commandText = "INSERT INTO [dbo].[Tags] ([TagID], [TagName], [IsDeleted]) VALUES ";

                    foreach (var tag in tags)
                    {
                        string curParam = "( @" + Convert.ToString(tag);
                        commandText += curParam + ", 0)\nINSERT INTO [dbo].[TagLinks] ([TagID], [ArticleID]) VALUES (IDENT_CURRENT('Tags'), @artId)\n";
                        insCommand.Parameters.AddWithValue(curParam, tag);
                    }

                    // need to fix
                    return true;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        public Dictionary<int,string> GetArticleTags(int artId)
        {
            Dictionary<int, string> resultEnum = new Dictionary<int, string>();

            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selCommand = (SqlCommand)currConnection.CreateCommand();
                    selCommand.CommandText = "SELECT TagID, (SELECT TagName FROM Tags t WHERE t.TagID = tl.TagID AND IsDeleted = 0) AS 'TagName' FROM TagLinks tl WHERE tl.ArticleID = @artId";
                    selCommand.Parameters.AddWithValue("@artId", artId);

                    using (IDataReader reader = selCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            resultEnum.Add((int)reader[TagLinksFields.TagID], reader["TagName"] as string);
                        }

                        return resultEnum;
                    }
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public Dictionary<int, string> GetAllTags()
        {
            Dictionary<int, string> resultEnum = new Dictionary<int, string>();

            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selCommand = (SqlCommand)currConnection.CreateCommand();
                    selCommand.CommandText = "SELECT * FROM Tags WHERE IsDeleted = 0";

                    using (IDataReader reader = selCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultEnum.Add((int)reader[TagsFields.TagID], reader[TagsFields.TagName] as string);
                        }

                        return resultEnum;
                    }
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public bool DeleteTag(int tagId)
        {

            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selCommand = (SqlCommand)currConnection.CreateCommand();
                    selCommand.CommandText = "UPDATE [dbo].[Tags] SET IsDeleted = 1 WHERE TagID = @tagId";
                    selCommand.Parameters.AddWithValue("@tagId", tagId);

                    return (selCommand.ExecuteNonQuery() == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        #endregion
 

        #region Themes table

        /// <summary>
        /// Use on creating article
        /// </summary>
        /// <returns></returns>
        public List<string> GetAvailArticleThemes()
        {
            try
            {
                List<string> result = new List<string>();

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selThemesComm = (SqlCommand)currConnection.CreateCommand();
                    selThemesComm.CommandText = "SELECT Theme FROM [dbo].[Themes]";

                    using (SqlDataReader reader = selThemesComm.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            result.Add(reader[ThemesFields.Theme] as string);
                        }
                    }
                }

                return result;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        /// <summary>
        /// Use in article search by theme view
        /// </summary>
        /// <returns></returns>
        public List<Themes> GetThemes()
        {
            try
            {
                List<Themes> result = new List<Themes>();
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selThemesComm = (SqlCommand)currConnection.CreateCommand();
                    selThemesComm.CommandText = "SELECT * FROM [dbo].[Themes]";

                    using (SqlDataReader reader = selThemesComm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Themes() { ThemeID = (int)reader[ThemesFields.ThemeID], Theme = reader[ThemesFields.Theme] as string });
                        }
                    }
                }

                return result;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public string GetTheme(int themeId)
        {
            try
            {
                List<Themes> result = new List<Themes>();
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selThemeComm = (SqlCommand)currConnection.CreateCommand();
                    selThemeComm.CommandText = "SELECT Theme FROM [dbo].[Themes] WHERE ThemeID = @themeId";
                    selThemeComm.Parameters.AddWithValue("@themeId", themeId);
                    return selThemeComm.ExecuteScalar() as string;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        #endregion


        #endregion

        #region CRUD methods for Users table

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <param name="email"></param>
        /// <param name="RoleID"></param>
        /// <param name="BlogID"></param>
        /// <returns> 0 for successful result, 1 if login already exist, 2 if email already exist, else -1</returns>
        public int CreateUser(string login, string password, string fname, string lname, string email, string phone)
        {
            try
            {
                if(!this.UserLoginNotAlreadyExist(login))
                {
                    this.UnresolvedExceptions.Add(new ArgumentException($"User with email = {email} already exist"));
                    return 1;
                }

                if (!this.UserEmailNotAlreadyExist(email))
                {
                    this.UnresolvedExceptions.Add(new ArgumentException($"User with login = {login} already exist"));
                    return 2;
                }

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand crUserComm = (SqlCommand)currConnection.CreateCommand();
                    crUserComm.CommandText = "INSERT INTO [dbo].[Users] ([Login], [Password], [FirstName], [LastName], [Email], [Phone], [RoleID], [BlogID], [RegistrationDate], [IsDeleted]) "
                        + "VALUES (@login, LOWER(CONVERT(NVARCHAR(MAX),HASHBYTES('md5',@password), 2)), @fname, @lname, @email, @phone, @roleId, @blogId, @regDate, @isDel)";

                    if(lname == string.Empty || phone == null)
                    {
                        crUserComm.Parameters.AddWithValue("@lname", DBNull.Value);
                    }
                    else
                    {
                        crUserComm.Parameters.AddWithValue("@lname", lname);
                    }

                    if (phone == string.Empty || phone == null)
                    {
                        crUserComm.Parameters.AddWithValue("@phone", DBNull.Value);
                    }
                    else
                    {
                        crUserComm.Parameters.AddWithValue("@phone", phone);
                    }

                    crUserComm.Parameters.AddWithValue("@blogId", DBNull.Value);
                    crUserComm.Parameters.AddWithValue("@isDel", 0);
                    crUserComm.Parameters.AddWithValue("@regDate", DateTime.Now.ToShortDateString());
                    crUserComm.Parameters.AddWithValue("@roleId", 2);
                    crUserComm.Parameters.AddWithValue("@login", login);
                    crUserComm.Parameters.AddWithValue("@password", password);
                    crUserComm.Parameters.AddWithValue("@fname", fname);
                    crUserComm.Parameters.AddWithValue("@email", email);

                    return (crUserComm.ExecuteNonQuery() == 1) ? 0 : -1;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return -1;
            }
        }


        /// <summary>
        /// Get actual users (IsDeleted field is false)
        /// </summary>
        /// <returns></returns>
        public List<Users> GetUsers()
        {
            List<Users> resultEnum = new List<Users>();
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "SELECT * FROM [dbo].[Users] WHERE IsDeleted = 0";

                    using (IDataReader reader = selectUsersComm.ExecuteReader())
                    {
                        Users currUser = new Users();

                        currUser.UserID = (int)reader[UsersFields.UserID];
                        currUser.Login = reader[UsersFields.Login] as string;
                        currUser.Password = reader[UsersFields.Password] as string;
                        currUser.FirstName = reader[UsersFields.FirstName] as string;
                        currUser.LastName = reader[UsersFields.LastName] as string;
                        currUser.Email = reader[UsersFields.Email] as string;
                        currUser.Phone = reader[UsersFields.Phone] as string;
                        currUser.RoleID = (int)reader[UsersFields.RoleID];
                        currUser.BlogID = reader[UsersFields.BlogID] as int?;
                        currUser.RegistrationDate = (DateTime)reader[UsersFields.RegistrationDate];
                        currUser.IsDeleted = (bool)reader[UsersFields.IsDeleted];

                        resultEnum.Add(currUser);
                    }
                }

                return resultEnum;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        /// <summary>
        /// Get user by login and password, i.e used as sign in (PL),
        /// all checks are carried out by separate methods in PL (for are availability login, ...),
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="resultUser"></param>
        /// <returns></returns>
        public Users GetUser(string login, string password)
        {
            try
            {
                var checker = this.CheckUserLoginPasswordPair(login, password);
                Users resultUser = new Users();

                switch (checker)
                {
                    case 1:

                        this.UnresolvedExceptions.Add(new ArgumentException($"User with login = {login} not already exist"));
                        return null;

                    case 2:

                        this.UnresolvedExceptions.Add(new ArgumentException($"Incorrect password for login = {login}"));
                        return null;

                    case -1:
                        return null;

                    case 0:

                        using (var currConnection = this.providerFactory.CreateConnection())
                        {

                            currConnection.ConnectionString = this.connectionString;
                            currConnection.Open();

                            SqlCommand selUserComm = (SqlCommand)currConnection.CreateCommand();
                            selUserComm.CommandText = "SELECT * FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = @login and Password = LOWER(CONVERT(NVARCHAR(MAX),HASHBYTES('md5',@password), 2))";
                            selUserComm.Parameters.AddWithValue("@login", login);
                            selUserComm.Parameters.AddWithValue("@password", password);

                            using (SqlDataReader reader = selUserComm.ExecuteReader())
                            {
                                reader.Read();

                                resultUser.UserID = (int)reader[UsersFields.UserID];
                                resultUser.Login = reader[UsersFields.Login] as string;
                                resultUser.Password = reader[UsersFields.Password] as string;
                                resultUser.FirstName = reader[UsersFields.FirstName] as string;
                                resultUser.LastName = reader[UsersFields.LastName] as string;
                                resultUser.Email = reader[UsersFields.Email] as string;
                                resultUser.Phone = reader[UsersFields.Phone] as string;
                                resultUser.RoleID = (int)reader[UsersFields.RoleID];
                                resultUser.BlogID = reader[UsersFields.BlogID] as int?;
                                resultUser.RegistrationDate = (DateTime)reader[UsersFields.RegistrationDate];
                                resultUser.IsDeleted = (bool)reader[UsersFields.IsDeleted];
                            }
                        }

                        return resultUser;
                }

                return resultUser;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public Users GetUser(string login)
        {
            try
            {
                var checker = this.UserLoginNotAlreadyExist(login);
                Users resultUser = new Users();

                switch (checker)
                {
                    case true:

                        this.UnresolvedExceptions.Add(new ArgumentException($"User with login = {login} not already exist"));
                        return null;

                    case false:

                        using (var currConnection = this.providerFactory.CreateConnection())
                        {

                            currConnection.ConnectionString = this.connectionString;
                            currConnection.Open();

                            SqlCommand selUserComm = (SqlCommand)currConnection.CreateCommand();
                            selUserComm.CommandText = "SELECT * FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = @login";
                            selUserComm.Parameters.AddWithValue("@login", login);

                            using (SqlDataReader reader = selUserComm.ExecuteReader())
                            {
                                reader.Read();

                                resultUser.UserID = (int)reader[UsersFields.UserID];
                                resultUser.Login = reader[UsersFields.Login] as string;
                                resultUser.Password = reader[UsersFields.Password] as string;
                                resultUser.FirstName = reader[UsersFields.FirstName] as string;
                                resultUser.LastName = reader[UsersFields.LastName] as string;
                                resultUser.Email = reader[UsersFields.Email] as string;
                                resultUser.Phone = reader[UsersFields.Phone] as string;
                                resultUser.RoleID = (int)reader[UsersFields.RoleID];
                                resultUser.BlogID = reader[UsersFields.BlogID] as int?;
                                resultUser.RegistrationDate = (DateTime)reader[UsersFields.RegistrationDate];
                                resultUser.IsDeleted = (bool)reader[UsersFields.IsDeleted];
                            }
                        }

                        return resultUser;
                }

                return resultUser;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public Dictionary<int,string> GetUserInfo(int blogId)
        {
            try
            {
                Dictionary<int, string> resultUser = new Dictionary<int, string>();

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selUserComm = (SqlCommand)currConnection.CreateCommand();
                    selUserComm.CommandText = "SELECT UserID, Login FROM [dbo].[Users] WHERE IsDeleted = 0 AND BlogID = @blogId";
                    selUserComm.Parameters.AddWithValue("@blogId", blogId);

                    using (SqlDataReader reader = selUserComm.ExecuteReader())
                    {
                        reader.Read();

                        return new Dictionary<int, string>() {
                            { (int)reader[UsersFields.UserID], reader[UsersFields.Login] as string } };
                    }
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public string GetUserLogin(int userId)
        {
            try
            {

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selUserComm = (SqlCommand)currConnection.CreateCommand();
                    selUserComm.CommandText = "SELECT Login FROM [dbo].[Users] WHERE IsDeleted = 0 AND UserID = @userId";
                    selUserComm.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = selUserComm.ExecuteReader())
                    {
                        reader.Read();

                        return selUserComm.ExecuteScalar() as string;
                    }
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        public int GetUserId(string login)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selUserComm = (SqlCommand)currConnection.CreateCommand();
                    selUserComm.CommandText = "SELECT UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = @login";
                    selUserComm.Parameters.AddWithValue("@login", login);
                    return (int)selUserComm.ExecuteScalar();
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return -1;
            }
        }

        /// <summary>
        /// Get all deleted users
        /// </summary>
        /// <returns></returns>
        public List<Users> GetDeletedUsers()
        {
            try
            {
                List<Users> resultEnum = new List<Users>();

                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "SELECT * FROM [dbo].[Users] WHERE IsDeleted = 1";

                    using (IDataReader reader = selectUsersComm.ExecuteReader())
                    {
                        Users currUser = new Users();

                        currUser.UserID = (int)reader[UsersFields.UserID];
                        currUser.Login = reader[UsersFields.Login] as string;
                        currUser.Password = reader[UsersFields.Password] as string;
                        currUser.FirstName = reader[UsersFields.FirstName] as string;
                        currUser.LastName = reader[UsersFields.LastName] as string;
                        currUser.Email = reader[UsersFields.Email] as string;
                        currUser.Phone = reader[UsersFields.Phone] as string;
                        currUser.RoleID = (int)reader[UsersFields.RoleID];
                        currUser.BlogID = reader[UsersFields.BlogID] as int?;
                        currUser.RegistrationDate = (DateTime)reader[UsersFields.RegistrationDate];
                        currUser.IsDeleted = (bool)reader[UsersFields.IsDeleted];

                        resultEnum.Add(currUser);
                    }
                }

                return resultEnum;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }


        /// <summary>
        /// Update User fields, it is necessary to finish the logic for catched exceptions
        /// </summary>
        /// <param name="newValues"> key - field name, key - new value</param>
        /// <param name="userId"> updated record id</param>
        /// <returns></returns>
        public bool UpdateUser(Dictionary<string, string> newValues, int userId)
        {
            try
            {
                if (!UsersFields.IsUsersTableFields(newValues.Keys))
                {
                    throw new ArgumentException("Input field name is not contain Users table fields");
                }

                foreach(var newVal in newValues)
                {
                    this.updateUsersField.First(kvp => kvp.Key == newVal.Key).Value.Invoke(userId, newVal.Value);
                }

                return true;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Set user administrator role
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <returns>query execute bool result</returns>
        public bool SetAdminRights(int userId)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Users] SET RoleID = @roleId WHERE UserID = @userId";
                    selectUsersComm.Parameters.AddWithValue("@roleId", StaticRoles.Administrator);
                    selectUsersComm.Parameters.AddWithValue("@userId", userId);

                    int chQueryRes = selectUsersComm.ExecuteNonQuery();
                    if (chQueryRes == 0)
                    {
                        throw new ArgumentException($"Record in Users table with id = {userId} is not exist");
                    }

                    return (chQueryRes == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        public bool IsAdmin(string login)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand chForAvailabComm = (SqlCommand)currConnection.CreateCommand();
                    chForAvailabComm.CommandText = "SELECT RoleID FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = @login";
                    chForAvailabComm.Parameters.AddWithValue("@login", login);
                    return ((int)chForAvailabComm.ExecuteScalar() == StaticRoles.Administrator) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Checks user by user id for are availability
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true if user not exist, false if exist</returns>
        public bool UserNotAlreadyExist(int userId)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand chForAvailabComm = (SqlCommand)currConnection.CreateCommand();
                    chForAvailabComm.CommandText = "SELECT TOP (1) UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND UserID = @userId";
                    chForAvailabComm.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = chForAvailabComm.ExecuteReader())
                    {
                        return (!reader.HasRows) ? true : false;
                    }
                }

                return false;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Checks login for are availability
        /// </summary>
        /// <param name="login"></param>
        /// <returns>true if login not exist, false if exist</returns>
        public bool UserLoginNotAlreadyExist(string login)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand chForAvailabComm = (SqlCommand)currConnection.CreateCommand();
                    chForAvailabComm.CommandText = "SELECT TOP (1) UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = @login";
                    chForAvailabComm.Parameters.AddWithValue("@login", login);

                    using (SqlDataReader reader = chForAvailabComm.ExecuteReader())
                    {
                        return (!reader.HasRows) ? true : false;
                    }
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Checks user by have blog for are availability
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true if user already haven't blog, false else</returns>
        public bool UserNotAlreadyHaveBlog(int userId)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand chForAvailabComm = (SqlCommand)currConnection.CreateCommand();
                    chForAvailabComm.CommandText = "SELECT BlogID FROM [dbo].[Users] WHERE IsDeleted = 0 AND UserID = @userId";
                    chForAvailabComm.Parameters.AddWithValue("@userId", userId);

                    return (chForAvailabComm.ExecuteScalar() is DBNull) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        public bool IsUserBlog(int blogId, string login)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand chForAvailabComm = (SqlCommand)currConnection.CreateCommand();
                    chForAvailabComm.CommandText = "SELECT UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND BlogID = @blogId AND Login = @login";
                    chForAvailabComm.Parameters.AddWithValue("@blogId",blogId);
                    chForAvailabComm.Parameters.AddWithValue("@login", login);

                    using (SqlDataReader reader = chForAvailabComm.ExecuteReader())
                    {
                        return (!reader.HasRows) ? true : false;
                    }
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        public bool IsUserArticle(int artId, string login)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand chForAvailabComm = (SqlCommand)currConnection.CreateCommand();
                    chForAvailabComm.CommandText = "SELECT UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = @login AND BlogID = (SELECT BlogID FROM [dbo].[Articles] WHERE ArticleID = @artId)";
                    chForAvailabComm.Parameters.AddWithValue("@artId", artId);
                    chForAvailabComm.Parameters.AddWithValue("@login", login);

                    using (SqlDataReader reader = chForAvailabComm.ExecuteReader())
                    {
                        return (!reader.HasRows) ? true : false;
                    }
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Check login - password pair for are availability
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>0 for successful check, 1 if user with login is not exist, 2 if password incorrect, else -1  </returns>
        public int CheckUserLoginPasswordPair(string login, string password)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand chForAvailabLoginComm = (SqlCommand)currConnection.CreateCommand();
                    chForAvailabLoginComm.CommandText = "SELECT TOP (1) UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = @login";
                    chForAvailabLoginComm.Parameters.AddWithValue("@login", login);
                    using (SqlDataReader reader = chForAvailabLoginComm.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return 1;
                        }
                    }

                    SqlCommand chLogPasComm = (SqlCommand)currConnection.CreateCommand();
                    chLogPasComm.CommandText = "SELECT TOP (1) UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = @login AND Password = LOWER(CONVERT(NVARCHAR(MAX), HASHBYTES('md5', @password), 2))";
                    chLogPasComm.Parameters.AddWithValue("@login", login);
                    chLogPasComm.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = chLogPasComm.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return 0;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return -1;
            }
        }

        /// <summary>
        /// Checks email for are availability
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if login not exist, false if exist</returns>
        public bool UserEmailNotAlreadyExist(string email)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand chForAvailabComm = (SqlCommand)currConnection.CreateCommand();
                    chForAvailabComm.CommandText = "SELECT Count(*) FROM [dbo].[Users] WHERE IsDeleted = 0 AND Email = @email";
                    chForAvailabComm.Parameters.AddWithValue("@email", email);

                    return((int)chForAvailabComm.ExecuteScalar() == 0) ? true:false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Update user first name
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <param name="newValue"> new value</param>
        /// <returns></returns>
        private bool UpdateUsersFirstName(int userId, string newValue)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Users] SET FirstName = @firstName WHERE UserID = @userId";
                    selectUsersComm.Parameters.AddWithValue("@firstName", newValue);
                    selectUsersComm.Parameters.AddWithValue("@userId", userId);

                    int chQueryRes = selectUsersComm.ExecuteNonQuery();
                    if(chQueryRes == 0)
                    {
                        throw new ArgumentException($"Record in Users table with id = {userId} is not exist");
                    }

                    return (chQueryRes == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Update user phone
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <param name="newValue"> new value</param>
        /// <returns></returns>
        private bool UpdateUsersPhone(int userId, string newValue)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Users] SET Phone = @phone WHERE UserId = @userId";
                    if (newValue == string.Empty)
                    {
                        selectUsersComm.Parameters.AddWithValue("@phone", DBNull.Value);
                    }
                    else
                    {
                        selectUsersComm.Parameters.AddWithValue("@phone", newValue);
                    }

                    selectUsersComm.Parameters.AddWithValue("@userId", userId);
                    int chQueryRes = selectUsersComm.ExecuteNonQuery();
                    if (chQueryRes == 0)
                    {
                        throw new ArgumentException($"Record in Users table with id = {userId} is not exist");
                    }

                    return (chQueryRes == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Update user email
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <param name="newValue"> new value</param>
        /// <returns></returns>
        private bool UpdateUsersEmail(int userId, string newValue)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Users] SET Email = @email WHERE UserID = @userId";
                    selectUsersComm.Parameters.AddWithValue("@email", newValue);
                    selectUsersComm.Parameters.AddWithValue("@userId", userId);

                    int chQueryRes = selectUsersComm.ExecuteNonQuery();
                    if (chQueryRes == 0)
                    {
                        throw new ArgumentException($"Record in Users table with id = {userId} is not exist");
                    }

                    return (chQueryRes == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <param name="newValue"> new value</param>
        /// <returns></returns>
        private bool UpdateUsersPassword(int userId, string newValue)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Users] SET Password = LOWER(CONVERT(NVARCHAR(MAX),HASHBYTES('md5',@password), 2)) WHERE UserID = @userId";
                    selectUsersComm.Parameters.AddWithValue("@password", newValue);
                    selectUsersComm.Parameters.AddWithValue("@userId", userId);

                    int chQueryRes = selectUsersComm.ExecuteNonQuery();
                    if (chQueryRes == 0)
                    {
                        throw new ArgumentException($"Record in Users table with id = {userId} is not exist");
                    }

                    return (chQueryRes == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Update user login
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <param name="newValue"> new value</param>
        /// <returns></returns>
        private bool UpdateUsersLogin(int userId, string newValue)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Users] SET Login = @login WHERE UserID = @userId";
                    selectUsersComm.Parameters.AddWithValue("@login", newValue);
                    selectUsersComm.Parameters.AddWithValue("@userId", userId);

                    int chQueryRes = selectUsersComm.ExecuteNonQuery();
                    if (chQueryRes == 0)
                    {
                        throw new ArgumentException($"Record in Users table with id = {userId} is not exist");
                    }

                    return (chQueryRes == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Update user last name
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <param name="newValue"> new value</param>
        /// <returns></returns>
        private bool UpdateUsersLastName(int userId, string newValue)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand selectUsersComm = (SqlCommand)currConnection.CreateCommand();
                    selectUsersComm.CommandText = "UPDATE [dbo].[Users] SET LastName = @lastName  WHERE UserID = @userId";
                    if (newValue == string.Empty)
                    {
                        selectUsersComm.Parameters.AddWithValue("@lastName", DBNull.Value);
                    }
                    else
                    {
                        selectUsersComm.Parameters.AddWithValue("@lastName", newValue);
                    }

                    selectUsersComm.Parameters.AddWithValue("@userId", userId);
                    int chQueryRes = selectUsersComm.ExecuteNonQuery();
                    if (chQueryRes == 0)
                    {
                        throw new ArgumentException($"Record in Users table with id = {userId} is not exist");
                    }

                    return (chQueryRes == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }

        /// <summary>
        /// Set Users record for userId IsDeleted field true value, i.e delete user
        /// </summary>
        /// <param name="userId"> user id</param>
        /// <returns></returns>
        public bool DeleteUser(int userId)
        {
            try
            {
                using (var currConnection = this.providerFactory.CreateConnection())
                {

                    currConnection.ConnectionString = this.connectionString;
                    currConnection.Open();

                    SqlCommand comm = (SqlCommand)currConnection.CreateCommand();
                    comm.CommandText = "UPDATE [dbo].[Users] SET IsDeleted = 1 WHERE UserID = @userId";
                    comm.Parameters.AddWithValue("@userId", userId);

                    return (comm.ExecuteNonQuery() == 1) ? true : false;
                }
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return false;
            }
        }
        #endregion


        /// <summary>
        /// Init field - update field method delegate key - value pairs in private member dictionary
        /// </summary>
        private void AddUpdateMethodsRefs()
        {
            this.updateUsersField = new Dictionary<string, Func<int, string, bool>>();
            this.updateBlogsField = new Dictionary<string, Func<int, string, bool>>();
            this.updateArticlesField = new Dictionary<string, Func<int, string, bool>>();

            /// Users table

            this.updateUsersField.Add(UsersFields.Login, new Func<int, string, bool>(this.UpdateUsersLogin));
            this.updateUsersField.Add(UsersFields.Password, new Func<int, string, bool>(this.UpdateUsersPassword));
            this.updateUsersField.Add(UsersFields.FirstName, new Func<int, string, bool>(this.UpdateUsersFirstName));
            this.updateUsersField.Add(UsersFields.LastName, new Func<int, string, bool>(this.UpdateUsersLastName));
            this.updateUsersField.Add(UsersFields.Email, new Func<int, string, bool>(this.UpdateUsersEmail));
            this.updateUsersField.Add(UsersFields.Phone, new Func<int, string, bool>(this.UpdateUsersPhone));

            /// Blogs table

            this.updateBlogsField.Add(BlogsFields.Title, new Func<int, string, bool>(this.UpdateBlogTitle));

            /// Article table

            this.updateArticlesField.Add(ArticlesFields.Title, new Func<int, string, bool>(this.UpdateArticlesTitle));
            this.updateArticlesField.Add(ArticlesFields.Content, new Func<int, string, bool>(this.UpdateArticlesContent));

            // Comments table

            // Добавить оставшиеся методы
        }
    }
}
