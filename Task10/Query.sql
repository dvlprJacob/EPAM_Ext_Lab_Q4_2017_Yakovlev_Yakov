USE NORTHWIND

/*
	1.1 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (������� ShippedDate) ������������ � ������� 
���������� � ShipVia >= 2. ������ �������� ���� ������ ���� ������ ��� ����� ������������ ����������, �������� ����������� ������ 
�Writing International Transact-SQL Statements� � Books Online ������ �Accessing and Changing Relational Data Overview�. 
���� ����� ������������ ����� ��� ���� �������. ������ ������ ����������� ������ ������� OrderID, ShippedDate � ShipVia.  
�������� ������ ���� �� ������ ������ � NULL-�� � ������� ShippedDate
*/

SELECT OrderID,
ShippedDate,
ShipVia 
FROM Orders 
WHERE CONVERT(DATE,ShippedDate,1)>=CONVERT(DATE,DATEFROMPARTS(1998,5,6),1)
and ShipVia>=2;

--	�.�. DATEFROMPARTS(1998,5,6) is not null - ������ �������� ������� � null:
-- (ShippedDate>=DATEFROMPARTS(1998,5,6) or ShippedDate is null) and ShipVia>=2;

/*
	1.2 �������� ������, ������� ������� ������ �������������� ������ �� ������� Orders. � ����������� ������� ����������� ��� ������� 
ShippedDate ������ �������� NULL ������ �Not Shipped� � ������������ ��������� ������� CAS�. ������ ������ ����������� ������ 
������� OrderID � ShippedDate.
*/

SELECT OrderID,
CASE 
WHEN ShippedDate IS NULL THEN 'Not Shipped'
END AS 'ShippedDate'
FROM Orders
WHERE ShippedDate IS NULL

/*
	1.3 ������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (ShippedDate) �� ������� ��� ���� ��� ������� 
��� �� ����������. � ������� ������ ������������� ������ ������� OrderID (������������� � Order Number) � 
ShippedDate (������������� � Shipped Date). � ����������� ������� ����������� ��� ������� ShippedDate ������ �������� NULL 
������ �Not Shipped�, ��� ��������� �������� ����������� ���� � ������� �� ���������.
*/

SELECT OrderID AS 'Order Number',
CASE WHEN ShippedDate IS NULL THEN 'Not Shipped'
     ELSE CONVERT(VARCHAR(8),ShippedDate,1)
END AS 'Shipped Date'
FROM Orders
WHERE CONVERT(DATETIME,ShippedDate,1)>CONVERT(DATETIME,DATEFROMPARTS(1998,5,6),1)
OR ShippedDate IS NULL

/*
	2.1 ������� �� ������� Customers ���� ����������, ����������� � USA � Canada. ������ ������� � ������ ������� ��������� IN. 
����������� ������� � ������ ������������ � ��������� ������ � ����������� �������. ����������� ���������� ������� �� ����� 
���������� � �� ����� ����������.
*/ 

SELECT ContactName,
Country
FROM Customers 
WHERE Country IN ('USA','Canada')
ORDER BY ContactName,Country

/*
	2.2 ������� �� ������� Customers ���� ����������, �� ����������� � USA � Canada. ������ ������� � ������� ��������� IN. 
����������� ������� � ������ ������������ � ��������� ������ � ����������� �������.
����������� ���������� ������� �� ����� ����������.
*/

SELECT ContactName,
Country
FROM Customers 
WHERE Country NOT IN ('USA','Canada')
ORDER BY ContactName

/*
	2.3 ������� �� ������� Customers ��� ������, � ������� ��������� ���������. ������ ������ ���� ��������� ������ ���� ��� � 
������ ������������ �� ��������. �� ������������ ����������� GROUP BY. ����������� ������ ���� ������� � ����������� �������.
*/

DESC SELECT DISTINCT Country
FROM Customers

/*
	3.1 ������� ��� ������ (OrderID) �� ������� Order Details (������ �� ������ �����������), ��� ����������� �������� � �����������
�� 3 �� 10 ������������ � ��� ������� Quantity � ������� Order Details. ������������ �������� BETWEEN. ������ ������ ����������� 
������ ������� OrderID. 
*/

SELECT DISTINCT OrderID
FROM [Order Details]
WHERE Quantity BETWEEN 3 AND 10

/*
	3.2 ������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� ����� �� ��������� b � g.
������������ �������� BETWEEN. ���������, ��� � ���������� ������� �������� Germany. ������ ������ ����������� ������ ������� CustomerID
� Country � ������������ �� Country.
*/

SELECT CustomerID,
Country
FROM Customers
WHERE Country BETWEEN ('b') AND ('gz')
ORDER BY Country

/*
	3.3 ������� ���� ���������� �� ������� Customers, � ������� �������� ������ ���������� �� ����� �� ��������� b � g, �� ���������
�������� BETWEEN. � ������� ����� �Execution Plan� ���������� ����� ������ ���������������� 3.2 ��� 3.3 � ��� ����� ���� ������
� ������ ���������� ���������� Execution Plan-a ��� ���� ���� ��������, ���������� ���������� Execution Plan ���� ������ � ������
� ���� ����������� � �� �� ����������� ���� ����� �� ������ � �� ������ ��������� ���� ��������� ���������. ������ ������ �����������
������ ������� CustomerID � Country � ������������ �� Country.
*/

SELECT CustomerID,
Country
FROM Customers
WHERE Country>='B%'
AND Country<='Gz%'
ORDER BY Country

/*
	3.2 ���������������� ���� ���, ��� ������� ������� ����������� ����������,
� ��������� ������ ���������� ����������� ������  ���������� ������� � ��������

	�� ����� ���������� ������� 3.2 :
...
         <QueryPlan CachedPlanSize="24" CompileTime="1" CompileCPU="1" CompileMemory="184">
...
           <RelOp AvgRowSize="32" EstimateCPU="0.00043212" EstimateIO="0.0112613" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="40" LogicalOp="Sort" NodeId="0" Parallel="false" PhysicalOp="Sort" EstimatedTotalSubtreeCost="0.016637">
...
               <RelOp AvgRowSize="32" EstimateCPU="0.0002571" EstimateIO="0.00460648" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="40" EstimatedRowsRead="91" LogicalOp="Clustered Index Scan" NodeId="1" Parallel="false" PhysicalOp="Clustered Index Scan" EstimatedTotalSubtreeCost="0.00486358" TableCardinality="91">
...

	�� ����� ���������� ������� 3.3
...
         <QueryPlan CachedPlanSize="24" CompileTime="1" CompileCPU="1" CompileMemory="184">
...
           <RelOp AvgRowSize="32" EstimateCPU="0.00043212" EstimateIO="0.0112613" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="40" LogicalOp="Sort" NodeId="0" Parallel="false" PhysicalOp="Sort" EstimatedTotalSubtreeCost="0.016637">
...
               <RelOp AvgRowSize="32" EstimateCPU="0.0002571" EstimateIO="0.00460648" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="40" EstimatedRowsRead="91" LogicalOp="Clustered Index Scan" NodeId="1" Parallel="false" PhysicalOp="Clustered Index Scan" EstimatedTotalSubtreeCost="0.00486358" TableCardinality="91">


	������� � LIKE ���������������� ���� ���������� :
...
         <QueryPlan CachedPlanSize="16" CompileTime="1" CompileCPU="1" CompileMemory="160">
...
           <RelOp AvgRowSize="32" EstimateCPU="0.000421596" EstimateIO="0.0112613" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="39" LogicalOp="Sort" NodeId="0" Parallel="false" PhysicalOp="Sort" EstimatedTotalSubtreeCost="0.0166265">
...
               <RelOp AvgRowSize="32" EstimateCPU="0.0002571" EstimateIO="0.00460648" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="39" EstimatedRowsRead="91" LogicalOp="Clustered Index Scan" NodeId="1" Parallel="false" PhysicalOp="Clustered Index Scan" EstimatedTotalSubtreeCost="0.00486358" TableCardinality="91">
*/

SELECT CustomerID,
Country
FROM Customers
WHERE Country LIKE '[b-g]%'
ORDER BY Country

/*
	4.1 � ������� Products ����� ��� �������� (������� ProductName), ��� ����������� ��������� 'chocolade'.
��������, ��� � ��������� 'chocolade' ����� ���� �������� ���� ����� 'c' � �������� - ����� ��� ��������,
������� ������������� ����� �������. ���������: ���������� ������� ������ ����������� 2 ������.
*/

SELECT ProductName
FROM Products
WHERE ProductName LIKE ('%cho[a-z]olade%')

/*
	5.1 ����� ����� ����� ���� ������� �� ������� Order Details � ������ ���������� ����������� ������� �
������ �� ���. ��������� ��������� �� ����� � ��������� � ����� 1 ��� ���� ������ money.
������ (������� Discount) ���������� ������� �� ��������� ��� ������� ������. ��� �����������
�������������� ���� �� ��������� ������� ���� ������� ������ �� ��������� � ������� UnitPrice ����.
����������� ������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'. 
*/

SELECT FORMAT(SUM((UnitPrice - (UnitPrice*Discount))*Quantity),'N','en-us') AS 'Totals'
from [Order Details]

/*
	5.2 �� ������� Orders ����� ���������� �������, ������� ��� �� ���� ����������
(�.�. � ������� ShippedDate ��� �������� ���� ��������). ������������ ��� ���� ������� ������ ��������
COUNT. �� ������������ ����������� WHERE � GROUP. 
*/

SELECT COUNT(CASE WHEN ShippedDate IS NULL THEN 1
			      ELSE NULL
			 END) AS 'Total not shipped'
FROM Orders

/*
	5.3 �� ������� Orders ����� ���������� ��������� ����������� (CustomerID), ��������� ������.
������������ ������� COUNT � �� ������������ ����������� WHERE � GROUP.
*/

SELECT COUNT(DISTINCT CustomerID)
FROM Customers

/*
	6.1 �� ������� Orders ����� ���������� ������� � ������������ �� �����. � ����������� ������� ����
����������� ��� ������� c ���������� Year � Total. �������� ����������� ������, ������� ���������
���������� ���� �������.
*/

SELECT YEAR(OrderDate) AS 'Year',
COUNT(OrderID) as 'Total'
FROM Orders
GROUP BY YEAR(OrderDate)

-- Test query :

SELECT COUNT(*) AS 'Total'
FROM Orders


/*
	6.2 �� ������� Orders ����� ���������� �������, c�������� ������ ���������. ����� ��� ����������
�������� � ��� ����� ������ � ������� Orders, ��� � ������� EmployeeID ������ �������� ��� �������
��������. � ����������� ������� ���� ����������� ������� � ������ �������� (������ ������������� ���
���������� ������������� LastName & FirstName. ��� ������ LastName & FirstName ������ ���� ��������
��������� �������� � ������� ��������� �������. ����� �������� ������ ������ ������������ ����������� ��
EmployeeID.) � ��������� ������� �Seller� � ������� c ����������� ������� ����������� �
��������� 'Amount'. ���������� ������� ������ ���� ����������� �� �������� ���������� �������.
*/

SELECT (SELECT CONCAT(FirstName,' ',LastName)
	    FROM Employees
	    WHERE Employees.EmployeeID = Orders.EmployeeID)  AS 'Seller',
COUNT(*) AS 'Amount'
FROM Orders
GROUP BY EmployeeID
ORDER BY 'Amount' DESC

-- C 6.3 - 6.5 �� ����������
/*
	6.6 �� ������� Employees ����� ��� ������� �������� ��� ������������, �.�. ���� �� ������ �������. ��������� ������� � ������� 
'User Name' (LastName) � 'Boss'. � �������� ������ ���� ��������� ����� �� ������� LastName.
��������� �� ��� �������� � ���� �������?
*/

SELECT empl1.LastName AS 'User Name',
empl2.LastName AS 'Boss'
FROM Employees empl1,Employees empl2
WHERE empl1.ReportsTo = empl2.EmployeeID

--	���, �� ���������, ��� ��� ������� � ������ �������� ���� ReportTo null


/*
	7.1 ���������� ���������, ������� ����������� ������ 'Western' (������� Region). ���������� ������� ������ ����������� ��� ����:
'LastName' �������� � �������� ������������� ���������� ('TerritoryDescription' �� ������� Territories). ������ ������ ������������
JOIN � ����������� FROM. ��� ����������� ������ ����� ��������� Employees � Territories ���� ������������ ����������� ��������� ���
���� Northwind. 
*/

SELECT LastName AS 'Last Name',
TerritoryDescription
FROM Employees INNER JOIN EmployeeTerritories ON Employees.EmployeeID=EmployeeTerritories.EmployeeID
	 INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
	 INNER JOIN Region ON Territories.RegionID = Region.RegionID 
	 AND Region.RegionDescription='Western'

/*
	8.1 ��������� � ����������� ������� ����� ���� ���������� �� ������� Customers � ��������� ���������� �� ������� �� ������� Orders.
������� �� ��������, ��� � ��������� ���������� ��� �������, �� ��� ����� ������ ���� �������� � ����������� �������.
����������� ���������� ������� �� ����������� ���������� �������.
*/

SELECT ContactName,
COUNT(Orders.CustomerID) AS 'Amount'
FROM Customers LEFT OUTER JOIN Orders ON Customers.CustomerID = Orders.CustomerID
GROUP BY Customers.CustomerID,ContactName
ORDER BY 'Amount'

/*
	9.1 ��������� ���� ����������� ������� CompanyName � ������� Suppliers, � ������� ��� ���� �� ������ �������� �� ������
(UnitsInStock � ������� Products ����� 0). ������������ ��������� SELECT ��� ����� ������� � �������������� ��������� IN.
����� �� ������������ ������ ��������� IN �������� '=' ?
*/

SELECT CompanyName
FROM Suppliers
WHERE Suppliers.SupplierID IN (SELECT Products.SupplierID 
							   FROM Products 
							   WHERE UnitsInStock=0)

--	������, ��� ��� ��������� ������ ���������� ��������� �������� � �� ����� ������������ � ���������� ID ���������� �� ���������

--	10.1 ��������� ���� ���������, ������� ����� ����� 150 �������. ������������ ��������� ��������������� SELECT.

select CONCAT(FirstName,' ',LastName) AS 'Employees with orders more than 150'
FROM Employees empl
WHERE EmployeeID IN (SELECT EmployeeID
					 FROM Orders 
					 GROUP BY EmployeeID
					 HAVING COUNT(Orders.EmployeeID)>150)

/*
	11.1 ��������� ���� ���������� (������� Customers), ������� �� ����� �� ������ ������ (��������� �� ������� Orders).
������������ ��������������� SELECT � �������� EXISTS.
Empty result
*/

SELECT ContactName AS 'Customers without orders'
FROM Customers
WHERE EXISTS (SELECT CustomerID
			  FROM Orders
			  GROUP BY CustomerID
			  HAVING COUNT(Orders.CustomerID) = 0)

/* 
	12.1 ��� ������������ ����������� ��������� Employees ��������� �� ������� Employees ������
������ ��� ���� ��������, � ������� ���������� ������� Employees (������� LastName ) �� ���� �������.
���������� ������ ������ ���� ������������ �� �����������.
*/

SELECT (SUBSTRING(LastName,1,1)) AS 'Employees last name first letter'
FROM Employees
ORDER BY 'Employees last name first letter'

-- Procedure execute examples :

--	13.1 SP_ShippedOrdersDiff :

EXECUTE [dbo].[SP_GreatestOrders] @Year = 1998, @Row = 10

--	13.2 SP_ShippedOrdersDiff :

EXECUTE [dbo].[SP_ShippedOrdersDiff] @ShippedDelay=14

--	With default param @ShippedDelay = 35 :

EXECUTE [dbo].[SP_ShippedOrdersDiff]

--	13.4 FN_IsBoss

-- Employee with ID 2 is not a boss (ReportTo column is NULL)

SELECT [dbo].[FN_IsBoss](2) AS 'Is boss'

-- For everyone

GO

DECLARE @ResTable TABLE (EmployeeID INT,Employee nvarchar(41),IsBoss BIT)

DECLARE Curr_empl CURSOR
FOR
SELECT FirstName,
LastName,
EmployeeID
FROM Employees

DECLARE @fname NVARCHAR(10)
DECLARE @lname NVARCHAR(20)
DECLARE @id INT

OPEN Curr_empl

FETCH NEXT FROM Curr_empl INTO @fname, @lname, @id
WHILE @@FETCH_STATUS = 0
BEGIN
     -- PRINT(CONCAT(@fname,' ',@lname,' - ',[dbo].[FN_IsBoss](@id))) - �� ����� ������ ������ �� �������, ������� :
     INSERT INTO @ResTable VALUES (@id,CONCAT(@fname,' ',@lname),[dbo].[FN_IsBoss](@id))
FETCH NEXT FROM Curr_empl INTO @fname, @lname, @id
END

CLOSE Curr_empl
DEALLOCATE Curr_empl


SELECT * FROM @ResTable
ORDER BY EmployeeID

