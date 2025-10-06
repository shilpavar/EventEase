-- To show all Services above the cost of 5 and how many services there are with the cost
SELECT ServiceCost, COUNT(*) AS CostCount
FROM ServicesTable
WHERE ServiceCost > 5
-- The GROUP BY  is used to arrange any identical data into groups (In this case ServiceCost)
GROUP BY ServiceCost

-- To get the max cost of service type 5
SELECT MAX(ServiceCost) AS MaxCost
FROM ServicesTable
WHERE ServiceType = 5

-- To get the min cost of service type 5
SELECT MIN(ServiceCost) AS MinCost
FROM ServicesTable
WHERE ServiceType = 5

-- To get the average cost of all the services
SELECT AVG(ServiceCost) AS AveragePrice
From ServicesTable

-- To eliminate  Duplicate data from the specified collumn 
SELECT DISTINCT (ServiceName)
FROM ServicesTable  

-- To count how many people there are in each gender column in the CustomerTable
SELECT CustomerGender, COUNT(CustomerGender) AS GenderCount
FROM CustomerTable
-- The GROUP BY  is used to arrange any identical data into groups (In this case CustomerGender)
GROUP BY CustomerGender
-- The ORDER BY is used to sort the results in DESC order
ORDER BY GenderCount DESC

-- Subquery To get the instructor and the average cost of their services
SELECT Instructor, ServiceName, ServiceCost,(SELECT AVG(ServiceCost) FROM ServicesTable)
FROM ServicesTable

-- Query To retrieve the Customers whose names start with M character
SELECT *
FROM CustomerTable
WHERE CustomerName LIKE 'M%'

-- The UNION query is used to combine the results from 2 different tables, I am using UNION ALL to allow duplicate values
SELECT CustomerName FROM CustomerTable
UNION ALL
SELECT UserName FROM UsersTable

-- To combine and return the 'Male Gender' and 'Female Gender' from the Customers Table and the Users table respectively
SELECT CustomerName, CustomerGender FROM CustomerTable
WHERE CustomerGender='Male'
UNION ALL
SELECT UserName, UserGender FROM UsersTable
WHERE UserGender = 'Female'
