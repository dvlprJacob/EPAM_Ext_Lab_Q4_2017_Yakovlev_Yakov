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
    using Helpers.TableFieldNames;
    using Helpers.StoredProcedureReturnFields;

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
                            order.OrderID = (int)reader[OrdersFields.OrderID];
                            order.CustomerID = reader[OrdersFields.CustomerID] as string;
                            order.EmployeeID = reader[OrdersFields.EmployeeID] as int?;
                            order.OrderDate = reader[OrdersFields.OrderDate] as DateTime?;
                            order.RequiredDate = reader[OrdersFields.RequiredDate] as DateTime?;
                            order.ShippedDate = reader[OrdersFields.ShippedDate] as DateTime?;
                            order.ShipVia = reader[OrdersFields.ShipVia] as int?;
                            order.Freight = reader[OrdersFields.Freight] as decimal?;
                            order.ShipName = reader[OrdersFields.ShipName] as string;
                            order.ShipAddress = reader[OrdersFields.ShipAddress] as string;
                            order.ShipCity = reader[OrdersFields.ShipCity] as string;
                            order.ShipRegion = reader[OrdersFields.ShipRegion] as string;
                            order.ShipPostalCode = reader[OrdersFields.ShipPostalCode] as string;
                            order.ShipCountry = reader[OrdersFields.ShipCountry] as string;

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

                    SqlCommand commandSelOrder = (SqlCommand)connection.CreateCommand();
                    commandSelOrder.CommandText = "SELECT * FROM [dbo].[Orders] WHERE OrderID = @id";
                    commandSelOrder.Parameters.AddWithValue("@id", orderId);

                    using (IDataReader reader = commandSelOrder.ExecuteReader())
                    {
                        reader.Read();

                        orderInfo.Order.OrderID = (int)reader[OrdersFields.OrderID];
                        orderInfo.Order.CustomerID = reader[OrdersFields.CustomerID] as string;
                        orderInfo.Order.EmployeeID = reader[OrdersFields.EmployeeID] as int?;
                        orderInfo.Order.OrderDate = reader[OrdersFields.OrderDate] as DateTime?;
                        orderInfo.Order.RequiredDate = reader[OrdersFields.RequiredDate] as DateTime?;
                        orderInfo.Order.ShippedDate = reader[OrdersFields.ShippedDate] as DateTime?;
                        orderInfo.Order.ShipVia = reader[OrdersFields.ShipVia] as int?;
                        orderInfo.Order.Freight = reader[OrdersFields.Freight] as decimal?;
                        orderInfo.Order.ShipName = reader[OrdersFields.ShipName] as string;
                        orderInfo.Order.ShipAddress = reader[OrdersFields.ShipAddress] as string;
                        orderInfo.Order.ShipCity = reader[OrdersFields.ShipCity] as string;
                        orderInfo.Order.ShipRegion = reader[OrdersFields.ShipRegion] as string;
                        orderInfo.Order.ShipPostalCode = reader[OrdersFields.ShipPostalCode] as string;
                        orderInfo.Order.ShipCountry = reader[OrdersFields.ShipCountry] as string;
                    }

                    var commandSelOrderDet = (SqlCommand)connection.CreateCommand();
                    commandSelOrderDet.Parameters.AddWithValue("@id", orderId);
                    commandSelOrderDet.CommandText = "SELECT * FROM [dbo].[Order Details] WHERE OrderID = @id";

                    using (IDataReader reader = commandSelOrderDet.ExecuteReader())
                    {
                        reader.Read();

                        orderInfo.Deatails.OrderID = orderId;
                        orderInfo.Deatails.ProductID = (int)reader[OrderDetailsFields.ProductID];
                        orderInfo.Deatails.UnitPrice = (decimal)reader[OrderDetailsFields.UnitPrice];
                        orderInfo.Deatails.Quantity = Convert.ToInt32(reader[OrderDetailsFields.Quantity]);
                        orderInfo.Deatails.Discount = Convert.ToDouble(reader[OrderDetailsFields.Discount]);
                    }

                    var commandSelProduct = (SqlCommand)connection.CreateCommand();
                    commandSelProduct.CommandText = "SELECT * FROM [dbo].[Products] WHERE ProductID = @prodId";
                    commandSelProduct.Parameters.AddWithValue("@prodId", orderInfo.Deatails.ProductID);

                    using (IDataReader reader = commandSelProduct.ExecuteReader())
                    {
                        reader.Read();

                        orderInfo.Product.ProductID = (int)reader[ProductsFields.ProductID];
                        orderInfo.Product.ProductName = (string)reader[ProductsFields.ProductName];
                        orderInfo.Product.SupplierID = reader[ProductsFields.SupplierID] as int?;
                        orderInfo.Product.CategoryID = reader[ProductsFields.CategoryID] as int?;
                        orderInfo.Product.QuantityPerUnit = (string)reader[ProductsFields.QuantityPerUnit];
                        orderInfo.Product.UnitPrice = reader[ProductsFields.UnitPrice] as decimal?;
                        orderInfo.Product.UnitsInStock = reader[ProductsFields.UnitsInStock] as int?;
                        orderInfo.Product.UnitsOnOrder = reader[ProductsFields.UnitsOnOrder] as int?;
                        orderInfo.Product.ReorderLevel = reader[ProductsFields.ReorderLevel] is System.DBNull ? null : (short?)(reader["ReorderLevel"]);
                        orderInfo.Product.Discontinued = (bool)reader[ProductsFields.Discontinued];
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
                            result.ProductName.Add((string)reader[CustOrderHistFields.ProductName]);
                            result.Total.Add((int)reader[CustOrderHistFields.Total]);
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
                        result.ProductName.Add((string)reader[CustOrdersDetailFields.ProductName]);
                        result.UnitPrice.Add((decimal)reader[CustOrdersDetailFields.UnitPrice]);
                        result.Quantity.Add((short)reader[CustOrdersDetailFields.Quantity]);
                        result.Discount.Add((int)reader[CustOrdersDetailFields.Discount]);
                        result.ExtendedPrice.Add((decimal)reader[CustOrdersDetailFields.ExtendedPrice]);
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