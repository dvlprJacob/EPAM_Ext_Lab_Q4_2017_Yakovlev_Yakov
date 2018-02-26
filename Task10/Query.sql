USE NORTHWIND

/*
	1.1 Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (колонка ShippedDate) включительно и которые 
доставлены с ShipVia >= 2. Формат указания даты должен быть верным при любых региональных настройках, согласно требованиям статьи 
“Writing International Transact-SQL Statements” в Books Online раздел “Accessing and Changing Relational Data Overview”. 
Этот метод использовать далее для всех заданий. Запрос должен высвечивать только колонки OrderID, ShippedDate и ShipVia.  
Пояснить почему сюда не попали заказы с NULL-ом в колонке ShippedDate
*/

SELECT OrderID,
ShippedDate,
ShipVia 
FROM Orders 
WHERE CONVERT(DATE,ShippedDate,1)>=CONVERT(DATE,DATEFROMPARTS(1998,5,6),1)
and ShipVia>=2;

--	т.к. DATEFROMPARTS(1998,5,6) is not null - способ включить картежи с null:
-- (ShippedDate>=DATEFROMPARTS(1998,5,6) or ShippedDate is null) and ShipVia>=2;

/*
	1.2 Написать запрос, который выводит только недоставленные заказы из таблицы Orders. В результатах запроса высвечивать для колонки 
ShippedDate вместо значений NULL строку ‘Not Shipped’ – использовать системную функцию CASЕ. Запрос должен высвечивать только 
колонки OrderID и ShippedDate.
*/

SELECT OrderID,
CASE 
WHEN ShippedDate IS NULL THEN 'Not Shipped'
END AS 'ShippedDate'
FROM Orders
WHERE ShippedDate IS NULL

/*
	1.3 Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) не включая эту дату или которые 
еще не доставлены. В запросе должны высвечиваться только колонки OrderID (переименовать в Order Number) и 
ShippedDate (переименовать в Shipped Date). В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL 
строку ‘Not Shipped’, для остальных значений высвечивать дату в формате по умолчанию.
*/

SELECT OrderID AS 'Order Number',
CASE WHEN ShippedDate IS NULL THEN 'Not Shipped'
     ELSE CONVERT(VARCHAR(8),ShippedDate,1)
END AS 'Shipped Date'
FROM Orders
WHERE CONVERT(DATETIME,ShippedDate,1)>CONVERT(DATETIME,DATEFROMPARTS(1998,5,6),1)
OR ShippedDate IS NULL

/*
	2.1 Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. Запрос сделать с только помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса. Упорядочить результаты запроса по имени 
заказчиков и по месту проживания.
*/ 

SELECT ContactName,
Country
FROM Customers 
WHERE Country IN ('USA','Canada')
ORDER BY ContactName,Country

/*
	2.2 Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada. Запрос сделать с помощью оператора IN. 
Высвечивать колонки с именем пользователя и названием страны в результатах запроса.
Упорядочить результаты запроса по имени заказчиков.
*/

SELECT ContactName,
Country
FROM Customers 
WHERE Country NOT IN ('USA','Canada')
ORDER BY ContactName

/*
	2.3 Выбрать из таблицы Customers все страны, в которых проживают заказчики. Страна должна быть упомянута только один раз и 
список отсортирован по убыванию. Не использовать предложение GROUP BY. Высвечивать только одну колонку в результатах запроса.
*/

DESC SELECT DISTINCT Country
FROM Customers

/*
	3.1 Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться), где встречаются продукты с количеством
от 3 до 10 включительно – это колонка Quantity в таблице Order Details. Использовать оператор BETWEEN. Запрос должен высвечивать 
только колонку OrderID. 
*/

SELECT DISTINCT OrderID
FROM [Order Details]
WHERE Quantity BETWEEN 3 AND 10

/*
	3.2 Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g.
Использовать оператор BETWEEN. Проверить, что в результаты запроса попадает Germany. Запрос должен высвечивать только колонки CustomerID
и Country и отсортирован по Country.
*/

SELECT CustomerID,
Country
FROM Customers
WHERE Country BETWEEN ('b') AND ('gz')
ORDER BY Country

/*
	3.3 Выбрать всех заказчиков из таблицы Customers, у которых название страны начинается на буквы из диапазона b и g, не используя
оператор BETWEEN. С помощью опции “Execution Plan” определить какой запрос предпочтительнее 3.2 или 3.3 – для этого надо ввести
в скрипт выполнение текстового Execution Plan-a для двух этих запросов, результаты выполнения Execution Plan надо ввести в скрипт
в виде комментария и по их результатам дать ответ на вопрос – по какому параметру было проведено сравнение. Запрос должен высвечивать
только колонки CustomerID и Country и отсортирован по Country.
*/

SELECT CustomerID,
Country
FROM Customers
WHERE Country>='B%'
AND Country<='Gz%'
ORDER BY Country

/*
	3.2 предпочтительнее лишь тем, что условие выборки описывается лаконичнее,
в остальном методы потребляют практически равное  количество времени и ресурсов

	Из плана выполнения запроса 3.2 :
...
         <QueryPlan CachedPlanSize="24" CompileTime="1" CompileCPU="1" CompileMemory="184">
...
           <RelOp AvgRowSize="32" EstimateCPU="0.00043212" EstimateIO="0.0112613" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="40" LogicalOp="Sort" NodeId="0" Parallel="false" PhysicalOp="Sort" EstimatedTotalSubtreeCost="0.016637">
...
               <RelOp AvgRowSize="32" EstimateCPU="0.0002571" EstimateIO="0.00460648" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="40" EstimatedRowsRead="91" LogicalOp="Clustered Index Scan" NodeId="1" Parallel="false" PhysicalOp="Clustered Index Scan" EstimatedTotalSubtreeCost="0.00486358" TableCardinality="91">
...

	Из плана выполнения запроса 3.3
...
         <QueryPlan CachedPlanSize="24" CompileTime="1" CompileCPU="1" CompileMemory="184">
...
           <RelOp AvgRowSize="32" EstimateCPU="0.00043212" EstimateIO="0.0112613" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="40" LogicalOp="Sort" NodeId="0" Parallel="false" PhysicalOp="Sort" EstimatedTotalSubtreeCost="0.016637">
...
               <RelOp AvgRowSize="32" EstimateCPU="0.0002571" EstimateIO="0.00460648" EstimateRebinds="0" EstimateRewinds="0" EstimatedExecutionMode="Row" EstimateRows="40" EstimatedRowsRead="91" LogicalOp="Clustered Index Scan" NodeId="1" Parallel="false" PhysicalOp="Clustered Index Scan" EstimatedTotalSubtreeCost="0.00486358" TableCardinality="91">


	Вариант с LIKE предпочтительнее двух предыдущих :
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
	4.1 В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'.
Известно, что в подстроке 'chocolade' может быть изменена одна буква 'c' в середине - найти все продукты,
которые удовлетворяют этому условию. Подсказка: результаты запроса должны высвечивать 2 строки.
*/

SELECT ProductName
FROM Products
WHERE ProductName LIKE ('%cho[a-z]olade%')

/*
	5.1 Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и
скидок по ним. Результат округлить до сотых и высветить в стиле 1 для типа данных money.
Скидка (колонка Discount) составляет процент из стоимости для данного товара. Для определения
действительной цены на проданный продукт надо вычесть скидку из указанной в колонке UnitPrice цены.
Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'. 
*/

SELECT FORMAT(SUM((UnitPrice - (UnitPrice*Discount))*Quantity),'N','en-us') AS 'Totals'
from [Order Details]

/*
	5.2 По таблице Orders найти количество заказов, которые еще не были доставлены
(т.е. в колонке ShippedDate нет значения даты доставки). Использовать при этом запросе только оператор
COUNT. Не использовать предложения WHERE и GROUP. 
*/

SELECT COUNT(CASE WHEN ShippedDate IS NULL THEN 1
			      ELSE NULL
			 END) AS 'Total not shipped'
FROM Orders

/*
	5.3 По таблице Orders найти количество различных покупателей (CustomerID), сделавших заказы.
Использовать функцию COUNT и не использовать предложения WHERE и GROUP.
*/

SELECT COUNT(DISTINCT CustomerID)
FROM Customers

/*
	6.1 По таблице Orders найти количество заказов с группировкой по годам. В результатах запроса надо
высвечивать две колонки c названиями Year и Total. Написать проверочный запрос, который вычисляет
количество всех заказов.
*/

SELECT YEAR(OrderDate) AS 'Year',
COUNT(OrderID) as 'Total'
FROM Orders
GROUP BY YEAR(OrderDate)

-- Test query :

SELECT COUNT(*) AS 'Total'
FROM Orders


/*
	6.2 По таблице Orders найти количество заказов, cделанных каждым продавцом. Заказ для указанного
продавца – это любая запись в таблице Orders, где в колонке EmployeeID задано значение для данного
продавца. В результатах запроса надо высвечивать колонку с именем продавца (Должно высвечиваться имя
полученное конкатенацией LastName & FirstName. Эта строка LastName & FirstName должна быть получена
отдельным запросом в колонке основного запроса. Также основной запрос должен использовать группировку по
EmployeeID.) с названием колонки ‘Seller’ и колонку c количеством заказов высвечивать с
названием 'Amount'. Результаты запроса должны быть упорядочены по убыванию количества заказов.
*/

SELECT (SELECT CONCAT(FirstName,' ',LastName)
	    FROM Employees
	    WHERE Employees.EmployeeID = Orders.EmployeeID)  AS 'Seller',
COUNT(*) AS 'Amount'
FROM Orders
GROUP BY EmployeeID
ORDER BY 'Amount' DESC

-- C 6.3 - 6.5 не разобрался
/*
	6.6 По таблице Employees найти для каждого продавца его руководителя, т.е. кому он делает репорты. Высветить колонки с именами 
'User Name' (LastName) и 'Boss'. В колонках должны быть высвечены имена из колонки LastName.
Высвечены ли все продавцы в этом запросе?
*/

SELECT empl1.LastName AS 'User Name',
empl2.LastName AS 'Boss'
FROM Employees empl1,Employees empl2
WHERE empl1.ReportsTo = empl2.EmployeeID

--	нет, не высвечены, так как минимум у одного продавца поле ReportTo null


/*
	7.1 Определить продавцов, которые обслуживают регион 'Western' (таблица Region). Результаты запроса должны высвечивать два поля:
'LastName' продавца и название обслуживаемой территории ('TerritoryDescription' из таблицы Territories). Запрос должен использовать
JOIN в предложении FROM. Для определения связей между таблицами Employees и Territories надо использовать графические диаграммы для
базы Northwind. 
*/

SELECT LastName AS 'Last Name',
TerritoryDescription
FROM Employees INNER JOIN EmployeeTerritories ON Employees.EmployeeID=EmployeeTerritories.EmployeeID
	 INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
	 INNER JOIN Region ON Territories.RegionID = Region.RegionID 
	 AND Region.RegionDescription='Western'

/*
	8.1 Высветить в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество их заказов из таблицы Orders.
Принять во внимание, что у некоторых заказчиков нет заказов, но они также должны быть выведены в результатах запроса.
Упорядочить результаты запроса по возрастанию количества заказов.
*/

SELECT ContactName,
COUNT(Orders.CustomerID) AS 'Amount'
FROM Customers LEFT OUTER JOIN Orders ON Customers.CustomerID = Orders.CustomerID
GROUP BY Customers.CustomerID,ContactName
ORDER BY 'Amount'

/*
	9.1 Высветить всех поставщиков колонка CompanyName в таблице Suppliers, у которых нет хотя бы одного продукта на складе
(UnitsInStock в таблице Products равно 0). Использовать вложенный SELECT для этого запроса с использованием оператора IN.
Можно ли использовать вместо оператора IN оператор '=' ?
*/

SELECT CompanyName
FROM Suppliers
WHERE Suppliers.SupplierID IN (SELECT Products.SupplierID 
							   FROM Products 
							   WHERE UnitsInStock=0)

--	Нельзя, так как вложенный запрос возвращает множество картежей и не может сравниваться с конкретным ID поставщика на равенство

--	10.1 Высветить всех продавцов, которые имеют более 150 заказов. Использовать вложенный коррелированный SELECT.

select CONCAT(FirstName,' ',LastName) AS 'Employees with orders more than 150'
FROM Employees empl
WHERE EmployeeID IN (SELECT EmployeeID
					 FROM Orders 
					 GROUP BY EmployeeID
					 HAVING COUNT(Orders.EmployeeID)>150)

/*
	11.1 Высветить всех заказчиков (таблица Customers), которые не имеют ни одного заказа (подзапрос по таблице Orders).
Использовать коррелированный SELECT и оператор EXISTS.
Empty result
*/

SELECT ContactName AS 'Customers without orders'
FROM Customers
WHERE EXISTS (SELECT CustomerID
			  FROM Orders
			  GROUP BY CustomerID
			  HAVING COUNT(Orders.CustomerID) = 0)

/* 
	12.1 Для формирования алфавитного указателя Employees высветить из таблицы Employees список
только тех букв алфавита, с которых начинаются фамилии Employees (колонка LastName ) из этой таблицы.
Алфавитный список должен быть отсортирован по возрастанию.
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
     -- PRINT(CONCAT(@fname,' ',@lname,' - ',[dbo].[FN_IsBoss](@id))) - Не понял почему ничего не выводит, поэтому :
     INSERT INTO @ResTable VALUES (@id,CONCAT(@fname,' ',@lname),[dbo].[FN_IsBoss](@id))
FETCH NEXT FROM Curr_empl INTO @fname, @lname, @id
END

CLOSE Curr_empl
DEALLOCATE Curr_empl


SELECT * FROM @ResTable
ORDER BY EmployeeID

