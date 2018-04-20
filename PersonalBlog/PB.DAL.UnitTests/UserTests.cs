using Microsoft.VisualStudio.TestTools.UnitTesting;
using PB.DAL.TableModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.DAL.UnitTests
{
    [TestClass]
    public class UserTests
    {
        private string connectionString;

        private DbProviderFactory providerFactory;

        private PbRepository repos;

        public UserTests()
        {
            this.repos = new PbRepository();
            var conStringTemp = ConfigurationManager.ConnectionStrings["PersonalBlogConnection"];
            this.connectionString = conStringTemp.ConnectionString;
            this.providerFactory = DbProviderFactories.GetFactory(conStringTemp.ProviderName);
        }

        [TestMethod]
        public void CreateTests()
        {
            this.repos = new PbRepository();
            var conStringTemp = ConfigurationManager.ConnectionStrings["PersonalBlogConnection"];
            this.connectionString = conStringTemp.ConnectionString;
            this.providerFactory = DbProviderFactories.GetFactory(conStringTemp.ProviderName);

            Assert.AreEqual(repos.CreateUser("admin", "12345", "user", "", "as@as.ru", "+998654265"),1);

            Assert.AreEqual(repos.CreateUser("new user", "12345", "user", "", "admin@my.com.ru", "+998654265"), 2);

            Assert.AreEqual(repos.CreateUser("new user", "password", "user", "", "nu@m.ru", string.Empty), 0);

            using (var currConnection = this.providerFactory.CreateConnection())
            {

                currConnection.ConnectionString = this.connectionString;
                currConnection.Open();

                SqlCommand comm = (SqlCommand)currConnection.CreateCommand();
                comm.CommandText = "DELETE FROM [dbo].[Users] WHERE UserID = @userId";
                comm.Parameters.AddWithValue("@userId", this.repos.GetUser("new user","password").UserID);

                Assert.AreEqual(comm.ExecuteNonQuery(), 1);
            }
        }

        [TestMethod]
        public void ReadTests()
        {
            this.repos = new PbRepository();
            var conStringTemp = ConfigurationManager.ConnectionStrings["PersonalBlogConnection"];
            this.connectionString = conStringTemp.ConnectionString;
            this.providerFactory = DbProviderFactories.GetFactory(conStringTemp.ProviderName);

            Assert.AreEqual(repos.GetUsers().Count, 2);
            Assert.AreEqual(repos.GetDeletedUsers(), 0);
            Assert.AreEqual(repos.GetUserInfo(1).First().Value, "admin");

            Users temp = repos.GetUser("user2", "password");
            Assert.AreEqual(temp.RoleID, 2);
            Assert.AreEqual(temp.LastName, "user2");
            Assert.AreEqual(temp.BlogID.HasValue, false);
        }

        [TestMethod]
        public void UpdateTests()
        {
            this.repos = new PbRepository();
            var conStringTemp = ConfigurationManager.ConnectionStrings["PersonalBlogConnection"];
            this.connectionString = conStringTemp.ConnectionString;
            this.providerFactory = DbProviderFactories.GetFactory(conStringTemp.ProviderName);

            Dictionary<string, string> param1 = new Dictionary<string, string>();
            param1.Add("FirstName", "fname");
            param1.Add("Phone", string.Empty);

            Assert.AreEqual(this.repos.UpdateUser(param1,3), true);

            Dictionary<string, string> param2 = new Dictionary<string, string>();
            param2.Add("LastName", "user id is not exist");
            int records = this.repos.GetUsers().Count + 1;
            Assert.AreEqual(this.repos.UpdateUser(param2, records), false);

            Dictionary<string, string> RbParams = new Dictionary<string, string>();
            RbParams.Add("FirstName", "user2");
            RbParams.Add("Phone", "+7(3412) 365 95 41");

            Assert.AreEqual(this.repos.UpdateUser(RbParams, 3), true);
        }

        [TestMethod]
        public void DeleteTest()
        {
            this.repos = new PbRepository();
            var conStringTemp = ConfigurationManager.ConnectionStrings["PersonalBlogConnection"];
            this.connectionString = conStringTemp.ConnectionString;
            this.providerFactory = DbProviderFactories.GetFactory(conStringTemp.ProviderName);

            Assert.AreEqual(repos.DeleteUser(3),true);

            using (var currConnection = this.providerFactory.CreateConnection())
            {

                currConnection.ConnectionString = this.connectionString;
                currConnection.Open();

                SqlCommand comm = (SqlCommand)currConnection.CreateCommand();
                comm.CommandText = "UPDATE [dbo].[Users] SET IsDeleted = 0 WHERE UserID = @userId";
                comm.Parameters.AddWithValue("@userId",3);

                Assert.AreEqual(comm.ExecuteNonQuery(), 1);
            }
        }
    }
}
