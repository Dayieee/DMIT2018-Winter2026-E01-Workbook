<Query Kind="Statements">
  <Connection>
    <ID>73024a29-0000-4644-9650-eb46c0f9f18e</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(local)</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <EncryptTraffic>true</EncryptTraffic>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>WestWind-2024</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>

//Run the example below in Chinook-2025
//Albums
//	.GroupBy(x => x.ReleaseYear)
//	.Select(g => g)
//	.Dump();
	
//Albums
//	.GroupBy(x => x.ReleaseYear)
//	.Select(g => new {
//		Year = g.Key,
//		Albums = g.ToList()
//	})
//	.Dump();
	
//WestWind-2024 connection
Products
	.GroupBy(x => x.CategoryID)
	.Select(g => new {
		CategoryID = g.Key,
		Products = g.Select(x => new {
						ProductID = x.ProductID,
						ProductName = x.ProductName
		})
	})
	.Dump();
	
Customers
	.GroupBy(x => x.Region)
	.Dump("Groups")
	.Select(g => new {
		Region = g.Key == null ? "Unknown" : g.Key,
		OrderCount = g.Sum(x => x.Orders.Count())
	})
	.Dump("Groups with Items");
	
Orders
	.Where(x => (Customers
					.Where(c => c.Region == "DF")
					.Select(x => x.CustomerID)).Contains(x.CustomerID))
	.Dump();
	
OrderDetails
	.GroupBy(x => x.Product.ProductName)
	.Select(g => new {
		Product = g.Key,
		TotalQuantity = g.Sum(x => x.Quantity),
		TotalSales = g.Sum(x => x.Quantity * x.UnitPrice)
	})
	.Dump();
	
Orders
	.GroupBy(x => x.Employee.FirstName + " " + x.Employee.LastName)
	.Select(g => new {
		SalesRep = g.Key,
		Orders = g.Select(x => new {
					OrderID = x.OrderID,
					OrderDate = x.OrderDate,
					Customer = x.Customer.CompanyName
		}).Take(5)
	})
	.Dump();
	
OrderDetails
	.AsEnumerable()
	//.GroupBy(x => x.Order.OrderDate.Value.Year)
	.Where(x => x.Order.OrderDate.HasValue)
	//.GroupBy(x => ((DateTime)x.Order.OrderDate).Year)
	.GroupBy(x => x.Order.OrderDate?.Year)
	.Select(g => new {
		Year = g.Key,
		Reveneu = g.Sum(x => x.Quantity * x.UnitPrice)
	})
	.Dump();
