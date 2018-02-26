-- 13 Разработка функций и процедур

use NORTHWIND;

/* 
	13.1 Написать процедуру, которая возвращает самый крупный заказ для каждого из продавцов за определенный год. В результатах не может
быть несколько заказов одного продавца, должен быть только один и самый крупный. В результатах запроса должны быть выведены следующие
колонки: колонка с именем и фамилией продавца (FirstName и LastName – пример: Nancy Davolio), номер заказа и его стоимость. В запросе
надо учитывать Discount при продаже товаров. Процедуре передается год, за который надо сделать отчет, и количество возвращаемых
записей. Результаты запроса должны быть упорядочены по убыванию суммы заказа. Процедура должна быть реализована с использованием
оператора SELECT и БЕЗ ИСПОЛЬЗОВАНИЯ КУРСОРОВ. Название функции соответственно GreatestOrders. Необходимо продемонстрировать
использование этих процедур. Также помимо демонстрации вызовов процедур в скрипте Query.sql надо написать отдельный ДОПОЛНИТЕЛЬНЫЙ
проверочный запрос для тестирования правильности работы процедуры GreatestOrders. Проверочный запрос должен выводить в удобном для
сравнения с результатами работы процедур виде для определенного продавца для всех его заказов за определенный указанный год в
результатах следующие колонки: имя продавца, номер заказа, сумму заказа. Проверочный запрос не должен повторять запрос, написанный в
процедуре, - он должен выполнять только то, что описано в требованиях по нему. ВСЕ ЗАПРОСЫ ПО ВЫЗОВУ ПРОЦЕДУР ДОЛЖНЫ БЫТЬ НАПИСАНЫ
В ФАЙЛЕ Query.sql – см. пояснение ниже в разделе «Требования к оформлению».
*/

GO
IF EXISTS (SELECT * FROM sys.objects
           WHERE object_id = OBJECT_ID('SP_GreatestOrders'))
DROP PROCEDURE SP_GreatestOrders;

GO
-- По гайду процедуры должны начинаться с префикса SP
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
	13.2 Написать процедуру, которая возвращает заказы в таблице Orders, согласно указанному сроку доставки в днях (разница между
OrderDate и ShippedDate).  В результатах должны быть возвращены заказы, срок которых превышает переданное значение или еще
недоставленные заказы. Значению по умолчанию для передаваемого срока 35 дней. Название процедуры ShippedOrdersDiff. Процедура
должна высвечивать следующие колонки: OrderID, OrderDate, ShippedDate, ShippedDelay (разность в днях между ShippedDate и
OrderDate), SpecifiedDelay (переданное в процедуру значение).  Необходимо продемонстрировать использование этой процедуры.
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
	13.3 Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, так и подчиненных его подчиненных. В качестве 
входного параметра функции используется EmployeeID. Необходимо распечатать имена подчиненных и выровнять их в тексте (использовать оператор PRINT) согласно 
иерархии подчинения. Продавец, для которого надо найти подчиненных также должен быть высвечен. Название процедуры SubordinationInfo. В качестве алгоритма для 
решения этой задачи надо использовать пример, приведенный в Books Online и рекомендованный Microsoft для решения подобного типа задач. Продемонстрировать 
использование процедуры. 
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
	13.4  Написать функцию, которая определяет, есть ли у продавца подчиненные. Возвращает тип данных BIT. В качестве входного параметра функции используется
	EmployeeID. Название функции IsBoss. Продемонстрировать использование функции для всех продавцов из таблицы Employees.
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