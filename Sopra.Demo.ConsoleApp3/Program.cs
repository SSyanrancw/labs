using System;
using System.Linq;
using System.Data;
using Sopra.Demo.ConsoleApp3.Models;


namespace Sopra.Demo.ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ejercicios();
        }

        static void Ejercicios()
        {
            var contex = new ModelNorthwind();

            // Clientes de USA
            var clientesUSA = contex.Customers
                .Where(r => r.Country == "USA")
                .Select(r => new {r.CompanyName, r.Country, r.Region})
                .ToList();

            foreach (var i in clientesUSA) Console.WriteLine($"Cliente: {i.CompanyName} - País: {i.Country} - Región: {i.Region}");

            Console.WriteLine("Fin de clientes de USA." + Environment.NewLine);

            // Proveedores (Suppliers) de BERLIN
            var provBERLIN = contex.Suppliers
                .Where(r => r.City == "Berlin")
                .Select(r => new { r.CompanyName, r.Country, r.City })
                .ToList();

            foreach (var i in provBERLIN) Console.WriteLine($"Proveedor: {i.CompanyName} - País: {i.Country} - Ciudad: {i.City}");

            Console.WriteLine("Fin de proveedores de Berlin." + Environment.NewLine);

            // Los empleados con ID 3, 5 y 8
            var empleados = contex.Employees
                .Where(r => new int[] { 3, 5, 8 }.Contains(r.EmployeeID))
                .Select(r => new { r.EmployeeID, r.FirstName, r.LastName })
                .OrderBy(r => r.EmployeeID)
                .ToList();

            foreach (var i in empleados) Console.WriteLine($"ID: {i.EmployeeID} - Nombre: {i.FirstName} - Apellidos: {i.LastName}");

            Console.WriteLine("Fin de empleados con ID 3, 5 y 8." + Environment.NewLine);

            // Productos con stock mayor de cero
            var prodStock = contex.Products
                .Where(r => r.UnitsInStock > 0)
                .Select(r => new { r.ProductID, r.ProductName, r.UnitsInStock })
                .OrderBy(r => r.UnitsInStock)
                .ToList();

            foreach (var i in prodStock) Console.WriteLine($"ID: {i.ProductID} - Producto: {i.ProductName} - Stock: {i.UnitsInStock}");

            Console.WriteLine("Fin de productos con stock mayor de cero." + Environment.NewLine);

            // Productos con stock mayor de cero de los proveedores con id 1, 3 y 5
            var prodStockProv = contex.Products
                .Where(r => r.UnitsInStock > 0 && new int?[] { 1, 3, 5 }.Contains(r.SupplierID))
                .Select(r => new { r.ProductID, r.ProductName, r.UnitsInStock, r.SupplierID })
                .OrderBy(r => r.SupplierID)
                .ToList();

            foreach (var i in prodStockProv) Console.WriteLine($"ID: {i.ProductID} - Producto: {i.ProductName} - Stock: {i.UnitsInStock} - ID prov: {i.SupplierID}");

            Console.WriteLine("Fin de productos con stock mayor de cero de los proveedores con id 1, 3 y 5." + Environment.NewLine);

            // Productos con precio mayor de 20 y menor de 90
            var prodEntre20y90 = contex.Products
                .Where(r => r.UnitPrice > 20 && r.UnitPrice < 90)
                .Select(r => new { r.ProductID, r.ProductName, r.UnitPrice })
                .OrderBy(r => r.UnitPrice)
                .ToList();

            foreach (var i in prodEntre20y90) Console.WriteLine($"ID: {i.ProductID} - Producto: {i.ProductName} - Precio: {i.UnitPrice}");

            Console.WriteLine("Fin de productos con precio mayor de 20 y menor de 90." + Environment.NewLine);

            // Pedidos entre 01.01.97 y 15.07.97
            var pedEntreFecha = contex.Orders
                .Where(r => r.OrderDate >= new DateTime(1997, 01, 01) && r.OrderDate <= new DateTime(1997, 07, 15))
                .Select(r => new { r.OrderID, r.OrderDate })
                .OrderBy(r => r.OrderDate)
                .ToList();

            foreach (var i in pedEntreFecha) Console.WriteLine($"ID: {i.OrderID} - Fecha de pedido: {i.OrderDate}");

            Console.WriteLine("Fin de pedidos entre 01.01.97 y 15.07.97." + Environment.NewLine);

            // Pedidos del 97 registrado por los empleados con id 1, 3, 4 y 8
            var ped97Empleados = contex.Orders
                .Where(r => r.OrderDate.Value.Year == 1997 && new int?[] { 1, 2, 3, 8 }.Contains(r.EmployeeID))
                .Select(r => new { r.OrderID, r.OrderDate, r.EmployeeID })
                .OrderBy(r => r.OrderDate)
                .ToList();

            foreach (var i in ped97Empleados) Console.WriteLine($"ID: {i.OrderID} - Fecha de pedido: {i.OrderDate} - ID Empleado: {i.EmployeeID}");

            Console.WriteLine("Fin de pedidos del 97 registrado por los empleados con id 1, 3, 4 y 8." + Environment.NewLine);

            // Pedidos de abril del 96
            var pedAbril96 = contex.Orders
                .Where(r => r.OrderDate.Value.Month == 04 && r.OrderDate.Value.Year == 1996)
                .Select(r => new { r.OrderID, r.OrderDate })
                .OrderBy(r => r.OrderDate)
                .ToList();

            foreach (var i in pedAbril96) Console.WriteLine($"ID: {i.OrderID} - Fecha de pedido: {i.OrderDate}");

            Console.WriteLine("Fin de pedidos de abril del 96." + Environment.NewLine);

            // Pedidos del realizados los días uno de cada mes del año 98
            var pedPrimer98 = contex.Orders
                .Where(r => r.OrderDate.Value.Day == 01 && r.OrderDate.Value.Year == 1998)
                .Select(r => new { r.OrderID, r.OrderDate })
                .OrderBy(r => r.OrderDate)
                .ToList();

            foreach (var i in pedPrimer98) Console.WriteLine($"ID: {i.OrderID} - Fecha de pedido: {i.OrderDate}");

            Console.WriteLine("Fin de pedidos realizados los días uno de cada mes del año 98." + Environment.NewLine);

            // Clientes que no tienen fax
            var clientesSinFax = contex.Customers
                .Where(r => r.Fax == null)
                .Select(r => new { r.CustomerID, r.CompanyName, r.Fax })
                .ToList();

            foreach (var i in clientesSinFax) Console.WriteLine($"ID: {i.CustomerID} - Cliente: {i.CompanyName} - Fax: {i.Fax}");

            Console.WriteLine("Fin de clientes que no tienen fax." + Environment.NewLine);

            // Los 10 productos más baratos
            var prodBaratos = contex.Products
                .Select(r => new { r.ProductID, r.ProductName, r.UnitPrice })
                .OrderBy(r => r.UnitPrice)
                .Take(10) 
                .ToList();

            foreach (var i in prodBaratos) Console.WriteLine($"ID: {i.ProductID} - Producto: {i.ProductName} - Precio: {i.UnitPrice}");

            Console.WriteLine("Fin de los 10 productos más baratos." + Environment.NewLine);

            // Los 10 productos más caros con stock
            var prodCarosStock = contex.Products
                .Select(r => new { r.ProductID, r.ProductName, r.UnitPrice, r.UnitsInStock })
                .Where(r => r.UnitsInStock > 0)
                .OrderByDescending(r => r.UnitPrice)
                .Take(10)
                .ToList();

            foreach (var i in prodCarosStock) Console.WriteLine($"ID: {i.ProductID} - Producto: {i.ProductName} - Precio: {i.UnitPrice} - Stock: {i.UnitsInStock}");

            Console.WriteLine("Fin de los 10 productos más caros en stock." + Environment.NewLine);

            // Clientes comience por la letra B en UK
            var clientesBUK = contex.Customers
                .Where(r => r.CompanyName.StartsWith("B") && r.Country == "UK")
                .Select(r => new { r.CustomerID, r.CompanyName, r.Country })
                .ToList();

            foreach (var i in clientesBUK) Console.WriteLine($"ID: {i.CustomerID} - Cliente: {i.CompanyName} - País: {i.Country}");

            Console.WriteLine("Fin de clientes que comience por la letra B en UK." + Environment.NewLine);

            // Productos de la categoria 3 y 5
            var prodCat = contex.Products
                .Where(r => new int?[] { 3, 5 }.Contains(r.CategoryID))
                .Select(r => new { r.ProductID, r.ProductName, r.CategoryID })
                .OrderBy(r => r.CategoryID)
                .ToList();

            foreach (var i in prodCat) Console.WriteLine($"ID: {i.ProductID} - Producto: {i.ProductName} - ID Categoría: {i.CategoryID}");

            Console.WriteLine("Fin de productos de la categoria 3 y 5." + Environment.NewLine);

            // Valor total del stock
            var totalStock = contex.Products
                .Sum(r => r.UnitsInStock * r.UnitPrice);

            Console.WriteLine($"Valor total del stock: {totalStock}");

            // Todos los pedidos de clientes de Argentina
            var customerID = contex.Customers
                .Where(r => r.Country == "Argentin")
                .Select(r => r.CustomerID)
                .ToList();

            var pedClienteArg = contex.Orders
                .Where(r => customerID.Contains(r.CustomerID))
                .Select(r => new { r.OrderID})
                .ToList();

            foreach (var i in pedClienteArg) Console.WriteLine($"ID Pedido: {i.OrderID}");

            Console.WriteLine("Fin de los pedidos de clientes de Argentina." + Environment.NewLine);
        }
    }
}
