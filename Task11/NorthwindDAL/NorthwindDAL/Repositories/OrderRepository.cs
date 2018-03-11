namespace NorthwindDAL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.IO;
    using NorthwindDAL.Helpers;
    using NorthwindDAL.Models;

    public class OrderRepository
    {
        private string connectionString;

        private DbProviderFactory providerFactory;

        public OrderRepository()
        {
            try
            {
                this.UnresolvedExceptions = new List<Exception>();
                var connectionStringSection = ConfigurationManager.ConnectionStrings["NorthwindConnection"];
                this.connectionString = connectionStringSection.ConnectionString;
                this.providerFactory = DbProviderFactories.GetFactory(connectionStringSection.ProviderName);
            }
            catch (IOException exc)
            {
                // ...
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
            }
        }

        public List<Exception> UnresolvedExceptions { get; set; }

        /// <summary>
        /// Return all records from Orders datatable with enum DeliveryStatus
        /// </summary>
        /// <returns></returns>
        public List<OrdersList> GetAllOrders()
        {
            var orders = new List<OrdersList>();

            try
            {
                using (var connection = this.providerFactory.CreateConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM [dbo].[Orders] ORDER BY OrderDate";

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

                            OrdersList temp = new OrdersList();
                            temp.AllOrders = order;
                            this.SetDeliveryStatus(temp);

                            orders.Add(temp);
                        }
                    }
                }

                return orders;
            }
            catch (SqlException exc)
            {
                // ...
                return null;
            }
            catch (InvalidCastException exc)
            {
                // ...
                return null;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        /// <summary>
        /// Return records from Orders, Order Details, Products for OrderID = orderId param
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public SummaryOrderData GetOrderInfo(int orderId)
        {
            var orderInfo = new SummaryOrderData();
            orderInfo.Order = new Orders();
            orderInfo.Deatails = new OrderDetails();
            orderInfo.Product = new Products();

            try
            {
                using (var connection = this.providerFactory.CreateConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    SqlCommand command = (SqlCommand)connection.CreateCommand();
                    command.CommandText = "SELECT * FROM [dbo].[Orders] WHERE OrderID = @id";
                    command.Parameters.AddWithValue("@id", orderId);

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

                    command.CommandText = "SELECT * FROM [dbo].[Order Details] WHERE OrderID = @id";

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        orderInfo.Deatails.OrderID = orderId;
                        orderInfo.Deatails.ProductID = (int)reader["ProductID"];
                        orderInfo.Deatails.UnitPrice = (decimal)reader["UnitPrice"];
                        orderInfo.Deatails.Quantity = Convert.ToInt32(reader["Quantity"]);
                        orderInfo.Deatails.Discount = Convert.ToDouble(reader["Discount"]);
                    }

                    command.Parameters.RemoveAt(0);
                    command.CommandText = "SELECT * FROM [dbo].[Products] WHERE ProductID = @prodId";
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
                }

                return orderInfo;
            }
            catch (SqlException exc)
            {
                // ...
                return null;
            }
            catch (InvalidCastException exc)
            {
                // Discontinued
                return null;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        /// <summary>
        /// Create new records in datatables Orders, Order Details for EmployeeID with the cheapest UnitPrice for ProductID
        /// </summary>
        /// <param name="newOrder"></param>
        public void CrateOrder(CreateOrderFields newOrder)
        {
            try
            {
                using (DbConnection connection = this.providerFactory.CreateConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();
                    var command = (SqlCommand)connection.CreateCommand();
                    command.Parameters.AddWithValue("@custId", newOrder.CustomerId);
                    command.Parameters.AddWithValue("@productId", newOrder.ProductID);
                    command.Parameters.AddWithValue("@quantity", newOrder.Quantity);
                    command.Parameters.AddWithValue("@discount", newOrder.Discount);

                    command.CommandText = "INSERT INTO[dbo].[Orders]([CustomerID], [EmployeeID], [ShipName], [ShipAddress],[ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry]) " +
                            "(SELECT @custId, (SELECT TOP 1 SupplierID FROM[dbo].[Products] " +
                            "WHERE ProductId = @productId ORDER BY UnitPrice), " +
                            "custs.CompanyName, custs.Address, custs.City, custs.Region, custs.PostalCode, custs.Country FROM[dbo].[Customers] AS custs " +
                            "WHERE custs.CustomerID = @custId) " +
                            "INSERT INTO dbo.[Order Details] (OrderID, ProductID, UnitPrice, Quantity, Discount) " +
                            "VALUES(IDENT_CURRENT('dbo.Orders'), @productId, (SELECT UnitPrice FROM[dbo].[Products] " +
                            "WHERE ProductID = @productId), @quantity, @discount)";

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exc)
            {
                // ...
            }
            catch (InvalidCastException exc)
            {
                // ...
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
            }
        }

        public void SetOrderDate(int orderId, DateTime date)
        {
            try
            {
                using (DbConnection connection = this.providerFactory.CreateConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var command = (SqlCommand)connection.CreateCommand();
                    command.CommandText = "UPDATE [dbo].[Orders] SET OrderDate = @date WHERE OrderID = @id";
                    command.Parameters.AddWithValue("@date", date.ToString("yyyy-dd-MM HH:mm:ss"));
                    command.Parameters.AddWithValue("@id", orderId);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exc)
            {
                // ...
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
            }
        }

        public void SetShippedDate(int orderId, DateTime date)
        {
            try
            {
                using (DbConnection connection = this.providerFactory.CreateConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var command = (SqlCommand)connection.CreateCommand();
                    command.CommandText = "UPDATE [dbo].[Orders] SET ShippedDate = @date WHERE OrderID = @id";
                    command.Parameters.AddWithValue("@date", date.ToString("yyyy-dd-MM HH:mm:ss"));
                    command.Parameters.AddWithValue("@id", orderId);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exc)
            {
                // ...
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
            }
        }

        /// <summary>
        /// Execute stored procedure CustOrderHist for customerId and return result
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustOrderHist GetCustOrderHist(string customerId)
        {
            try
            {
                var result = new CustOrderHist();
                result.ProductName = new List<string>();
                result.Total = new List<int>();

                result.CustomerID = customerId;
                using (DbConnection connection = this.providerFactory.CreateConnection())
                {
                    connection.ConnectionString = this.connectionString;
                    connection.Open();

                    var command = (SqlCommand)connection.CreateCommand();
                    command.Parameters.AddWithValue("@custId", customerId);
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

                return result;
            }
            catch (SqlException exc)
            {
                // ...
                return null;
            }
            catch (InvalidCastException exc)
            {
                // ...
                return null;
            }
            catch (Exception exc)
            {
                this.UnresolvedExceptions.Add(exc);
                return null;
            }
        }

        /// <summary>
        /// Execute stored procedure CustOrdersDeatil for orderId and return result
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public CustOrdersDetail GetCustOrdersDetail(int orderId)
        {
            var result = new CustOrdersDetail();
            result.Discount = new List<int>();
            result.ExtendedPrice = new List<decimal>();
            result.ProductName = new List<string>();
            result.Quantity = new List<short>();
            result.UnitPrice = new List<decimal>();

            result.OrderId = orderId;
            using (DbConnection connection = this.providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                var command = (SqlCommand)connection.CreateCommand();
                command.Parameters.AddWithValue("@orderId", orderId);
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

            return result;
        }

        private void SetDeliveryStatus(OrdersList order)
        {
            if (order.AllOrders.OrderDate == null)
            {
                order.OrderStatus = DeliveryStatus.NotSent;
            }

            if (order.AllOrders.ShippedDate == null)
            {
                order.OrderStatus = DeliveryStatus.OnWay;
            }

            if (order.AllOrders.OrderDate != null)
            {
                order.OrderStatus = DeliveryStatus.Done;
            }
        }
    }
}