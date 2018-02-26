-- 13 ���������� ������� � ��������

use NORTHWIND;

/* 
	13.1 �������� ���������, ������� ���������� ����� ������� ����� ��� ������� �� ��������� �� ������������ ���. � ����������� �� �����
���� ��������� ������� ������ ��������, ������ ���� ������ ���� � ����� �������. � ����������� ������� ������ ���� �������� ���������
�������: ������� � ������ � �������� �������� (FirstName � LastName � ������: Nancy Davolio), ����� ������ � ��� ���������. � �������
���� ��������� Discount ��� ������� �������. ��������� ���������� ���, �� ������� ���� ������� �����, � ���������� ������������
�������. ���������� ������� ������ ���� ����������� �� �������� ����� ������. ��������� ������ ���� ����������� � ��������������
��������� SELECT � ��� ������������� ��������. �������� ������� �������������� GreatestOrders. ���������� ������������������
������������� ���� ��������. ����� ������ ������������ ������� �������� � ������� Query.sql ���� �������� ��������� ��������������
����������� ������ ��� ������������ ������������ ������ ��������� GreatestOrders. ����������� ������ ������ �������� � ������� ���
��������� � ������������ ������ �������� ���� ��� ������������� �������� ��� ���� ��� ������� �� ������������ ��������� ��� �
����������� ��������� �������: ��� ��������, ����� ������, ����� ������. ����������� ������ �� ������ ��������� ������, ���������� �
���������, - �� ������ ��������� ������ ��, ��� ������� � ����������� �� ����. ��� ������� �� ������ �������� ������ ���� ��������
� ����� Query.sql � ��. ��������� ���� � ������� ����������� � �����������.
*/

GO
IF EXISTS (SELECT * FROM sys.objects
           WHERE object_id = OBJECT_ID('SP_GreatestOrders'))
DROP PROCEDURE SP_GreatestOrders;

GO
-- �� ����� ��������� ������ ���������� � �������� SP
CREATE PROCEDURE SP_GreatestOrders 
@Year INT,
@Row INT
AS BEGIN
DECLARE @JoinedTables TABLE (EmplID INT,OrderID INT,Cost FLOAT);
INSERT INTO @JoinedTables
SELECT empl.EmployeeID,
ord.OrderID,
SUM(ordDet.Quantity * ROUND((ordDet.UnitPrice - ordDet.UnitPrice * ordDet.Discount),2)) AS Cost
FROM Employees AS empl JOIN Orders ord
                       ON empl.EmployeeID = ord.EmployeeID
					   JOIN [Order Details] ordDet
					   ON ord.OrderID = ordDet.OrderID
WHERE YEAR(ord.OrderDate) = @Year
GROUP BY empl.EmployeeID, ord.OrderID;

SELECT TOP(@Row) CONCAT(FirstName,' ',LastName) AS Employee,
(SELECT TOP(1) OrderID
FROM @JoinedTables
WHERE EmplID = empl.EmployeeID
ORDER BY Cost DESC) AS OrderID,
(SELECT TOP(1) Cost
FROM @JoinedTables
WHERE EmplID = empl.EmployeeID
ORDER BY Cost DESC) AS Cost
FROM Employees AS empl
ORDER BY Cost DESC
END

/* 
	13.2 �������� ���������, ������� ���������� ������ � ������� Orders, �������� ���������� ����� �������� � ���� (������� �����
OrderDate � ShippedDate).  � ����������� ������ ���� ���������� ������, ���� ������� ��������� ���������� �������� ��� ���
�������������� ������. �������� �� ��������� ��� ������������� ����� 35 ����. �������� ��������� ShippedOrdersDiff. ���������
������ ����������� ��������� �������: OrderID, OrderDate, ShippedDate, ShippedDelay (�������� � ���� ����� ShippedDate �
OrderDate), SpecifiedDelay (���������� � ��������� ��������).  ���������� ������������������ ������������� ���� ���������.
*/

GO
IF EXISTS (SELECT * FROM sys.objects
           WHERE object_id = OBJECT_ID('SP_ShippedOrdersDiff'))
DROP PROCEDURE SP_ShippedOrdersDiff;

GO
CREATE PROCEDURE SP_ShippedOrdersDiff
@ShippedDelay INT = 35
AS BEGIN
SELECT Orders.OrderID,
CONVERT(NVARCHAR(8), OrderDate, 1) AS OrderDate,
CASE WHEN ShippedDate IS NOT NULL THEN CONVERT(NVARCHAR(8), ShippedDate, 1)
     ELSE 'Not Shipped' 
END AS ShippedDate,
CASE WHEN ShippedDate IS NOT NULL THEN DATEDIFF(dd, OrderDate, ShippedDate)
     ELSE 0 
END AS ActualDelay,
@ShippedDelay AS InputDelay
FROM dbo.Orders
WHERE (ShippedDate - OrderDate) > @ShippedDelay OR ShippedDate IS NULL
ORDER BY ActualDelay DESC, OrderID;
END

/*
	13.3 �������� ���������, ������� ����������� ���� ����������� ��������� ��������, ��� ����������������, ��� � ����������� ��� �����������. � �������� 
�������� ��������� ������� ������������ EmployeeID. ���������� ����������� ����� ����������� � ��������� �� � ������ (������������ �������� PRINT) �������� 
�������� ����������. ��������, ��� �������� ���� ����� ����������� ����� ������ ���� ��������. �������� ��������� SubordinationInfo. � �������� ��������� ��� 
������� ���� ������ ���� ������������ ������, ����������� � Books Online � ��������������� Microsoft ��� ������� ��������� ���� �����. ������������������ 
������������� ���������. 
*/

GO
IF EXISTS (SELECT * FROM sys.objects
           WHERE object_id = OBJECT_ID('SP_SubordinationInfo'))
DROP PROCEDURE SP_SubordinationInfo;

GO
CREATE PROCEDURE SP_SubordinationInfo
@EmplID INT
AS BEGIN

DECLARE @Empl NVARCHAR(31)
SET @Empl = (SELECT CONCAT(FirstName,' ',LastName)
             FROM Employees
			 WHERE EmployeeID=@EmplID)

PRINT @Empl;

DECLARE @Sub NVARCHAR(31)
SET @Sub = (SELECT CONCAT(sub.FirstName,' ',sub.LastName) AS Subordinate
            FROM Employees boss,Employees sub
            WHERE boss.ReportsTo = sub.EmployeeID)
--
END

 /*
	13.4  �������� �������, ������� ����������, ���� �� � �������� �����������. ���������� ��� ������ BIT. � �������� �������� ��������� ������� ������������
	EmployeeID. �������� ������� IsBoss. ������������������ ������������� ������� ��� ���� ��������� �� ������� Employees.
*/

GO
IF EXISTS (SELECT * FROM sys.objects
           WHERE object_id = OBJECT_ID('FN_IsBoss'))
DROP FUNCTION FN_IsBoss;

GO
CREATE FUNCTION FN_IsBoss(@EmployeeID INT)
RETURNS BIT
AS BEGIN

DECLARE @Check INT = 0
SET @Check = (SELECT COUNT(*)
              FROM Employees
			  WHERE EmployeeID = @EmployeeID
			  AND ReportsTo IS NOT NULL)

IF (@Check>0) RETURN 1
RETURN 0

END