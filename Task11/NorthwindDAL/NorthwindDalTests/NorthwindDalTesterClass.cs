namespace NorthwindDalTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NorthwindDAL;
    using System.Data.Common;
    using System.Configuration;
    using NorthwindDAL.Helpers;
    using NorthwindDAL.Models;
    using System.Data.SqlClient;
    using System.Data;
    using System.Collections.Generic;

    [TestClass]
    public class NorthwindDalTesterClass
    {
        private string connectionString;
        private DbProviderFactory providerFactory;
        private OrderRepository repository;

        public NorthwindDalTesterClass()
        {
            var connectionStringSection = ConfigurationManager.ConnectionStrings["NorthwindConnection"];
            this.connectionString = connectionStringSection.ConnectionString;
            this.providerFactory = DbProviderFactories.GetFactory(connectionStringSection.ProviderName);
            this.repository = new OrderRepository();
        }

        [TestMethod]
        public void GetAllOrdersTest()
        {
            var temp1 = this.repository.GetAllOrders();
            List<Orders> temp2 = new List<Orders>();

            using (DbConnection connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT * FROM [dbo].Orders ORDER BY OrderDate";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Orders order = new Orders();
                        order.OrderID = (int)reader["OrderID"];
                        order.CustomerID = reader["CustomerID"] as string;
                        order.EmployeeID = reader["EmployeeID"] as int?;
                        order.OrderDate = reader["OrderDate"] as DateTime?;
                        order.RequiredDate = reader["RequiredDate"] as DateTime?;
                        order.ShippedDate = reader["ShippedDate"] as DateTime?;
                        order.ShipVia = reader["ShipVia"] as int?;
                        order.Freight = reader["Freight"] as decimal?;
                        order.ShipName = reader["ShipName"] as string;
                        order.ShipAddress = reader["ShipAddress"] as string;
                        order.ShipCity = reader["ShipCity"] as string;
                        order.ShipRegion = reader["ShipRegion"] as string;
                        order.ShipPostalCode = reader["ShipPostalCode"] as string;
                        order.ShipCountry = reader["ShipCountry"] as string;

                        temp2.Add(order);
                    }
                }

                Assert.AreEqual(temp1.Count, temp2.Count);

                if (temp1.Count == temp2.Count)
                {
                    for (int i = 0; i < temp1.Count; i++)
                    {
                        Assert.AreEqual(temp1[i].AllOrders.OrderID, temp2[i].OrderID);
                    }
                }
            }
        }

        [TestMethod]
        public void GetOrderInfoTest()
        {
            int id;
            using (var connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                SqlCommand command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT TOP 1 OrderID FROM [dbo].[Orders] ORDER BY OrderID";
                id = (int)command.ExecuteScalar();

                command.CommandText = "SELECT * FROM [dbo].[Orders] WHERE OrderID = @id";
                command.Parameters.AddWithValue("@id", id);
                var orderInfo = new SummaryOrderData();
                orderInfo.Order = new Orders();
                orderInfo.Deatails = new OrderDetails();
                orderInfo.Product = new Products();

                using (IDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    orderInfo.Order.OrderID = (int)reader["OrderID"];
                    orderInfo.Order.CustomerID = reader["CustomerID"] as string;
                    orderInfo.Order.EmployeeID = reader["EmployeeID"] as int?;
                    orderInfo.Order.OrderDate = reader["OrderDate"] as DateTime?;
                    orderInfo.Order.RequiredDate = reader["RequiredDate"] as DateTime?;
                    orderInfo.Order.ShippedDate = reader["ShippedDate"] as DateTime?;
                    orderInfo.Order.ShipVia = reader["ShipVia"] as int?;
                    orderInfo.Order.Freight = reader["Freight"] as decimal?;
                    orderInfo.Order.ShipName = reader["ShipName"] as string;
                    orderInfo.Order.ShipAddress = reader["ShipAddress"] as string;
                    orderInfo.Order.ShipCity = reader["ShipCity"] as string;
                    orderInfo.Order.ShipRegion = reader["ShipRegion"] as string;
                    orderInfo.Order.ShipPostalCode = reader["ShipPostalCode"] as string;
                    orderInfo.Order.ShipCountry = reader["ShipCountry"] as string;
                }

                var temp1 = this.repository.GetOrderInfo(id);

                Assert.AreEqual(temp1.Order.OrderID, orderInfo.Order.OrderID);

                command.CommandText = "SELECT * FROM [dbo].[Order Details] WHERE OrderID = @id";

                using (IDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    orderInfo.Deatails.OrderID = id;
                    orderInfo.Deatails.ProductID = (int)reader["ProductID"];
                    orderInfo.Deatails.UnitPrice = (decimal)reader["UnitPrice"];
                    orderInfo.Deatails.Quantity = Convert.ToInt32(reader["Quantity"]);
                    orderInfo.Deatails.Discount = Convert.ToDouble(reader["Discount"]);
                }

                Assert.AreEqual(temp1.Deatails.OrderID, orderInfo.Deatails.OrderID);

                command.Parameters.RemoveAt(0);
                command.CommandText = "SELECT * FROM[dbo].[Products] WHERE ProductID = @prodId";
                command.Parameters.AddWithValue("@prodId", orderInfo.Deatails.ProductID);

                using (IDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    orderInfo.Product.ProductID = (int)reader["ProductID"];
                    orderInfo.Product.ProductName = (string)reader["ProductName"];
                    orderInfo.Product.SupplierID = reader["SupplierID"] as int?;
                    orderInfo.Product.CategoryID = reader["CategoryID"] as int?;
                    orderInfo.Product.QuantityPerUnit = (string)reader["QuantityPerUnit"];
                    orderInfo.Product.UnitPrice = reader["UnitPrice"] as decimal?;
                    orderInfo.Product.UnitsInStock = reader["UnitsInStock"] as int?;
                    orderInfo.Product.UnitsOnOrder = reader["UnitsOnOrder"] as int?;
                    orderInfo.Product.ReorderLevel = reader["ReorderLevel"] is System.DBNull ? null : (short?)(reader["ReorderLevel"]);
                    orderInfo.Product.Discontinued = (bool)reader["Discontinued"];
                }

                Assert.AreEqual(temp1.Product.ProductID, orderInfo.Product.ProductID);
            }
        }

        [TestMethod]
        public void CreateOrderTest()
        {
            using (DbConnection connection = this.providerFactory.CreateConnection())
            {
                CreateOrderFields newOrder = new CreateOrderFields();
                var rand = new Random();
                newOrder.Discount = 0.1;
                newOrder.Quantity = rand.Next(1, 25);

                connection.ConnectionString = this.connectionString;
                connection.Open();
                var insertCommand = (SqlCommand)connection.CreateCommand();

                insertCommand.CommandText = "SELECT Count(*) FROM [dbo].[Orders]";
                var ordersCount = (int)insertCommand.ExecuteScalar();

                insertCommand.CommandText = "SELECT Count(*) FROM [dbo].[Order Details]";
                var ordersDetCount = (int)insertCommand.ExecuteScalar();

                var insertData = (SqlCommand)connection.CreateCommand();
                insertData.CommandText = "SELECT TOP 1 CustomerID FROM [dbo].[Orders]";
                string custId = (string)insertData.ExecuteScalar();

                insertData.CommandText = "SELECT TOP 1 ProductID FROM [dbo].[Products]";
                int prodId = (int)insertData.ExecuteScalar();

                newOrder.CustomerId = custId;
                newOrder.ProductID = prodId;

                this.repository.CrateOrder(newOrder);

                insertData.CommandText = "SELECT Count(*) FROM [dbo].[Orders]";
                var ordersCountRes = (int)insertData.ExecuteScalar();

                insertData.CommandText = "SELECT Count(*) FROM [dbo].[Order Details]";
                var ordersDetCountRes = (int)insertData.ExecuteScalar();

                Assert.AreEqual(ordersCount + 1, ordersCountRes);
                Assert.AreEqual(ordersDetCount + 1, ordersDetCountRes);

                var id = (SqlCommand)connection.CreateCommand();
                id.CommandText = "SELECT IDENT_CURRENT('dbo.Orders')";

                var deleteCommand = (SqlCommand)connection.CreateCommand();
                deleteCommand.CommandText = "DELETE FROM [Order Details] WHERE OrderID = @id " +
                    "DELETE FROM Orders WHERE OrderID = @id";
                deleteCommand.Parameters.AddWithValue("@id", Convert.ToInt32(id.ExecuteScalar()));
                deleteCommand.ExecuteNonQuery();

                var ordDetRecordsCount = (int)insertData.ExecuteScalar();
                insertData.CommandText = "SELECT Count(*) FROM [dbo].[Orders]";
                var ordRecordsCount = (int)insertData.ExecuteScalar();

                Assert.AreEqual(ordDetRecordsCount, ordersDetCount);
                Assert.AreEqual(ordRecordsCount, ordersCount);
            }
        }

        [TestMethod]
        public void SetOrderDateTest()
        {
            using (DbConnection connection = this.providerFactory.CreateConnection())
            {
                var date = new DateTime();
                date = DateTime.Now;

                connection.ConnectionString = this.connectionString;
                connection.Open();

                //  ident_curent(‘Orders’) возвращал id не существующей записи, из-за чего selectedDate инициализировалось некорректно
                var commandSelOrderId = (SqlCommand)connection.CreateCommand();
                commandSelOrderId.CommandText = "SELECT TOP 1 OrderID FROM [dbo].[Orders] WHERE OrderDate IS NULL";
                var orderId = Convert.ToInt32(commandSelOrderId.ExecuteScalar());

                this.repository.SetOrderDate(orderId, date);

                var commandCheckRecord = (SqlCommand)connection.CreateCommand();
                commandCheckRecord.Parameters.AddWithValue("@id", orderId);
                commandCheckRecord.CommandText = "SELECT DAY(OrderDate), MONTH(OrderDate), YEAR(OrderDate) FROM Orders WHERE OrderID = @id";
                DateTime selectedDate = new DateTime();
                using (IDataReader reader = commandCheckRecord.ExecuteReader())
                {
                    reader.Read();
                    selectedDate = new DateTime((int)reader[2], (int)reader[1], (int)reader[0]);
                }

                Assert.AreEqual(selectedDate.Year, date.Year);
                Assert.AreEqual(selectedDate.Month, date.Month);
                Assert.AreEqual(selectedDate.Day, date.Day);

                var commandRollback = (SqlCommand)connection.CreateCommand();
                commandRollback.CommandText = "UPDATE [dbo].[Orders] SET OrderDate = NULL WHERE OrderID = @id";
                commandRollback.Parameters.AddWithValue("@id", orderId);
                commandRollback.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void SetShippedDateTest()
        {
            using (DbConnection connection = this.providerFactory.CreateConnection())
            {
                var date = new DateTime();
                date = DateTime.Now;

                connection.ConnectionString = this.connectionString;
                connection.Open();

                //  ident_curent(‘Orders’) возвращал id не существующей записи, из-за чего selectedDate инициализировалось некорректно
                var commandSelOrderId = (SqlCommand)connection.CreateCommand();
                commandSelOrderId.CommandText = "SELECT TOP 1 OrderID FROM [dbo].[Orders] WHERE ShippedDate IS NULL";
                var orderId = Convert.ToInt32(commandSelOrderId.ExecuteScalar());

                this.repository.SetShippedDate(orderId, date);

                var commandCheckRecord = (SqlCommand)connection.CreateCommand();
                commandCheckRecord.Parameters.AddWithValue("@id", orderId);
                commandCheckRecord.CommandText = "SELECT DAY(ShippedDate), MONTH(ShippedDate), YEAR(ShippedDate) FROM Orders WHERE OrderID = @id";
                DateTime selectedDate = new DateTime();
                using (IDataReader reader = commandCheckRecord.ExecuteReader())
                {
                    reader.Read();
                    selectedDate = new DateTime((int)reader[2], (int)reader[1], (int)reader[0]);
                }

                Assert.AreEqual(selectedDate.Year, date.Year);
                Assert.AreEqual(selectedDate.Month, date.Month);
                Assert.AreEqual(selectedDate.Day, date.Day);

                var commandRollback = (SqlCommand)connection.CreateCommand();
                commandRollback.CommandText = "UPDATE [dbo].[Orders] SET ShippedDate = NULL WHERE OrderID = @id";
                commandRollback.Parameters.AddWithValue("@id", orderId);
                commandRollback.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void GetCustOrderHistTest()
        {
            var result = new CustOrderHist();
            result.ProductName = new List<string>();
            result.Total = new List<int>();
            var resExecMethod = new CustOrderHist();
            resExecMethod.ProductName = new List<string>();
            resExecMethod.Total = new List<int>();

            using (DbConnection connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.CommandText = "SELECT TOP 1 CustomerID FROM [dbo].[Customers]";
                result.CustomerID = (string)command.ExecuteScalar();
                resExecMethod = this.repository.GetCustOrderHist(result.CustomerID);

                command.Parameters.AddWithValue("@custId", result.CustomerID);
                command.CommandText = "EXECUTE [dbo].[CustOrderHist] @CustomerID = @custId";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.ProductName.Add((string)reader["ProductName"]);
                        result.Total.Add((int)reader["Total"]);
                    }
                }
            }

            Assert.AreEqual(resExecMethod.CustomerID, result.CustomerID);
            Assert.AreEqual(resExecMethod.ProductName[0], result.ProductName[0]);
            Assert.AreEqual(resExecMethod.Total[0], result.Total[0]);
        }

        [TestMethod]
        public void GetCustOrdersDetailTest()
        {
            var result = new CustOrdersDetail();
            result.Discount = new List<int>();
            result.ExtendedPrice = new List<decimal>();
            result.ProductName = new List<string>();
            result.Quantity = new List<short>();
            result.UnitPrice = new List<decimal>();

            var resExecMethod = new CustOrdersDetail();

            using (DbConnection connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();

                //  SELECT TOP 1 OrderID FROM [dbo].[Orders] - вернул OrderID записи = 11079, у которого все поля дат null :
                //  11079;DUMON;5;NULL;NULL;NULL;NULL;0,00;Du monde entier;67, rue des Cinquante Otages;Nantes;NULL;44000;France
                //  Добавление условия выборки "WHERE OrderDate IS NOT NULL" исправило ситуацию
                command.CommandText = "SELECT TOP 1 OrderID FROM [dbo].[Orders] WHERE OrderDate IS NOT NULL";
                result.OrderId = (int)command.ExecuteScalar();
                resExecMethod = this.repository.GetCustOrdersDetail(result.OrderId);

                command.Parameters.AddWithValue("@orderId", result.OrderId);
                command.CommandText = "EXECUTE [dbo].[CustOrdersDetail] @OrderID = @orderId";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.ProductName.Add((string)reader["ProductName"]);
                        result.UnitPrice.Add((decimal)reader["UnitPrice"]);
                        result.Quantity.Add((short)reader["Quantity"]);
                        result.Discount.Add((int)reader["Discount"]);
                        result.ExtendedPrice.Add((decimal)reader["ExtendedPrice"]);
                    }
                }
            }

            Assert.AreEqual(resExecMethod.OrderId, result.OrderId);
            Assert.AreEqual(resExecMethod.ProductName[0], result.ProductName[0]);
            Assert.AreEqual(resExecMethod.Quantity[0], result.Quantity[0]);
            Assert.AreEqual(resExecMethod.UnitPrice[0], result.UnitPrice[0]);
            Assert.AreEqual(resExecMethod.ExtendedPrice[0], result.ExtendedPrice[0]);
        }
    }
}