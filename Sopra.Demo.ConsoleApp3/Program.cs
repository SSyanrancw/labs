using System;
using System.Linq;
using System.Data;
using Sopra.Demo.ConsoleApp3.Models;
using Microsoft.EntityFrameworkCore;


namespace Sopra.Demo.ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BusquedasBasicas();
            BusquedasComplejas();
        }

        static void BusquedasBasicas()
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

            Console.WriteLine($"Valor total del stock: {totalStock}" + Environment.NewLine);

            // Todos los pedidos de clientes de Argentina
            var iDCliente = contex.Customers
                .Where(r => r.Country == "Argentina")
                .Select(r => r.CustomerID);

            var pedClienteArg = contex.Orders
                .Where(r => iDCliente.Contains(r.CustomerID))
                .Select(r => new { r.OrderID, r.CustomerID})
                .ToList();

            // Versión anidada
            var r17 = contex.Orders
                .Where(r => contex.Customers
                    .Where(s => s.Country == "Argentina")
                    .Select(s => s.CustomerID)
                    .Contains(r.CustomerID))
                .ToList();

            foreach (var i in pedClienteArg) Console.WriteLine($"ID Pedido: {i.OrderID} - ID Cliente: {i.CustomerID}");

            Console.WriteLine("Fin de los pedidos de clientes de Argentina." + Environment.NewLine);

            // Listado de empleados que son mayores que sus jefes (ReportsTo es ID del jefe)
            var empMayorJefe = contex.Employees
                .Where(r => r.BirthDate < contex.Employees
                    .Where(s => s.EmployeeID == r.EmployeeID)
                    .Select(s => s.BirthDate)
                    .FirstOrDefault())
                .Select(r => new {r.EmployeeID, r.ReportsTo, r.BirthDate})
                .ToList();

            foreach (var i in empMayorJefe) Console.WriteLine($"ID Empleado: {i.EmployeeID} - ID Jefe: {i.ReportsTo} - Fecha de nacimiento: {i.BirthDate}");

            Console.WriteLine("Fin de listados de empleados mayores que sus jefes." + Environment.NewLine);

            // Listado de productos: nombre del producto, stock y valor del stock
            var prodStockTotal = contex.Products
                .Select(r => new { r.ProductName, r.UnitsInStock, Total = r.UnitsInStock * r.UnitPrice })
                .ToList();

            foreach (var i in prodStockTotal) Console.WriteLine($"Producto: {i.ProductName} - Stock: {i.UnitsInStock} - Valor stock: {i.Total}");

            Console.WriteLine("Fin de lista de productos." + Environment.NewLine);

            // Listado de empleados: nombre, apellido y número total de pedidos en 1997
            var empPedidos97 = contex.Employees
                .Select(r => new
                {
                    r.FirstName,
                    r.LastName,
                    Pedidos = contex.Orders.Count(s => s.EmployeeID == r.EmployeeID && s.OrderDate.Value.Year == 1997)
                })
                .ToList();

            foreach (var i in empPedidos97) Console.WriteLine($"Nombre: {i.FirstName} - Apellidos: {i.LastName} - Pedidos: {i.Pedidos}");

            Console.WriteLine("Fin de listados de empleados con sus pedidos del 97." + Environment.NewLine);

            // El tiempo medio en días de la preparación de pedido (Fecha de pedido - Fecha de envío)
            var tiempoMedioPrep = contex.Orders
                .Where(r => r.ShippedDate.HasValue && r.OrderDate.HasValue)
                .AsEnumerable()
                .Average(r => (r.ShippedDate - r.OrderDate).Value.Days);
            
            Console.WriteLine($"Tiempo medio en días para la preparación de pedidos: {tiempoMedioPrep} días" + Environment.NewLine);
        }

        static void BusquedasComplejas()
        {
            var contex = new ModelNorthwind();

            // Productos de la categoria Condiments y Seafood
            var prodCat = contex.Products
                .Include(r => r.Category)
                .Where(r => r.Category.CategoryName == "Condiments" || r.Category.CategoryName == "Seafood")
                .Select(r => new { r.ProductID, r.ProductName, Categ = r.Category.CategoryName })
                .ToList();

            foreach (var i in prodCat) Console.WriteLine($"ID: {i.ProductID} - Producto: {i.ProductName} - ID Categoría: {i.Categ}");

            Console.WriteLine("Fin de productos de la categoria Condiments y Seafood." + Environment.NewLine);

            // Listado de empleados: nombre, apellido y número total de pedidos en 1997
            var empPedidos97 = contex.Employees
                .Include(r => r.Orders)
                .Select(r => new 
                {   
                    r.FirstName, 
                    r.LastName, 
                    Pedidos = r.Orders.Count(s => s.OrderDate.Value.Year == 1997)
                })
                .ToList();

            foreach (var i in empPedidos97) Console.WriteLine($"Nombre: {i.FirstName} - Apellidos: {i.LastName} - Pedidos: {i.Pedidos}");

            Console.WriteLine("Fin de listados de empleados con sus pedidos del 97." + Environment.NewLine);

            // Listado de pedidos de los clientes de USA
            var pedUSA = contex.Orders
                .Include(r => r.Customer)
                .Where(r => r.Customer.Country == "USA")
                .Select(r => new {r.OrderID, r.CustomerID, Pais = r.Customer.Country})
                .ToList();

            foreach (var i in pedUSA) Console.WriteLine($"ID Pedido: {i.OrderID} - ID Cliente: {i.CustomerID} - País del cliente{i.Pais}");

            Console.WriteLine("Fin de los pedidos de clientes de USA." + Environment.NewLine);

            // Clientes que han pedido el producto 57
            var clientesPed57 = contex.Order_Details
                .Include(r => r.Order)
                .ThenInclude(r => r.Customer)
                .Where(r => r.ProductID == 57)
                .Select(r => new { Cliente = r.Order.Customer.CompanyName, r.ProductID, ClienteID = r.Order.CustomerID })
                .ToList();

            string[] a = new string[] { };

            foreach (var i in clientesPed57) {
                a = a.Concat(new string[] { i.ClienteID }).ToArray();
                Console.WriteLine($"ID Producto: {i.ProductID} - ID cLiente: {i.ClienteID} - Cliente: {i.Cliente}");
            }

            Console.WriteLine("Fin de los clientes que han pedido el producto 57." + Environment.NewLine);

            // Clientes que han pedido el producto 72 en 1997
            var clientesPed57en97 = contex.Order_Details
                .Include(r => r.Order)
                .ThenInclude(r => r.Customer)
                .Where(r => r.ProductID == 72 && r.Order.OrderDate.Value.Year ==  1997)
                .Select(r => new { Cliente = r.Order.Customer.CompanyName, r.ProductID, Fecha = r.Order.OrderDate, ClienteID = r.Order.CustomerID })
                .ToList();

            string[] b = new string[] { };

            foreach (var i in clientesPed57en97)
            {
                b = b.Concat(new string[] { i.ClienteID }).ToArray();
                Console.WriteLine($"ID Producto: {i.ProductID} - ID cLiente: {i.ClienteID} - Cliente: {i.Cliente} - Fecha de pedido: {i.Fecha}");
            }

            Console.WriteLine("Fin de los clientes que han pedido el producto 72 en 1997." + Environment.NewLine);

            // Clientes 57 + 72 en 1997          
            var clientes57y72 = a.Intersect(b).ToList();

            foreach (var i in clientes57y72) Console.WriteLine(i);
            Console.WriteLine("Fin de la intersección de las dos anteriores listas." + Environment.NewLine);
        }
    }
}
