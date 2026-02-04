<Query Kind="Statements">
  <Connection>
    <ID>59ce3865-da45-4116-a90c-bcb90bbfbb74</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>(local)</Server>
    <Database>Contoso</Database>
    <DisplayName>Contoso</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

//Take-Home Practice
//Take Home 2: Count()
ProductCategories
	 .Select(x => new {
	 	Category = x.ProductCategoryName,
		SubCategories = x.ProductSubcategories.Count()
	 })
	 .OrderBy(x => x.Category)
	 .ToList()
	 .Dump();
	 
//Take Home 4: Sum()
//Geographies
//	.Where(x => x.RegionCountryName == "Canada")
//	.Select(x => new {
//		City = x.CityName,
//		TotalIncome1 = x.Customers.Sum(c => c.YearlyIncome) > 0 ?
//			x.Customers.Sum(c => c.YearlyIncome) : 0,
//		TotalIncome2 = x.Customers.Sum(c => c.YearlyIncome) ?? 0	
//	})
//	.Where(x => x.City != null)
//	.OrderBy(x => x.City)
//	.ToList()
//	.Dump();
Geographies
	.Where(x => x.RegionCountryName == "Canada" && x.CityName != null)
	.Select(x => new {
		City = x.CityName,
		TotalIncome1 = x.Customers.Sum(c => c.YearlyIncome) > 0 ?
			x.Customers.Sum(c => c.YearlyIncome) : 0,
		TotalIncome2 = x.Customers.Sum(c => c.YearlyIncome) ?? 0	
	})
	.OrderBy(x => x.City)
	.ToList()
	.Dump();
	
//Question 6: Min()
Products
	.Select(x => new {
		ProductName = x.ProductName,
		//MinSalesQuantity = x.InvoiceLines.Min(il => il.SalesQuantity) ?? 0	
		MinSalesQuantity = x.InvoiceLines.Min(il => il.SalesQuantity) > 0 ?
			x.InvoiceLines.Min(il => il.SalesQuantity) : 0	
	})
	.ToList()
	.Dump();
	
//Question 8: Max()
Products
	.Select(x => new {
		Name = x.ProductName,
		ProductNo = x.ProductLabel,
		MaxDays1 = x.Inventories.Max(i => i.MaxDayInStock) > 0 ?
			x.Inventories.Max(i => i.MaxDayInStock).ToString() : "N/A",
		MaxDays2 = x.Inventories.Max(i => i.MaxDayInStock) ?? 0
	})
	.OrderBy(x => x.Name)
	.ToList()
	.Dump();
	
//Question 10: Average()
Customers
	.Where(x => x.Invoices.Sum(i => i.TotalAmount) > 0)
	.Select(x => new {
		CustomerID = x.CustomerID,
		//Name = $"{x.FirstName} {x.LastName}",
		Name = x.FirstName + " " + x.LastName,
		AveragePurchases = x.Invoices.Average(i => i.TotalAmount)
	})
	.OrderBy(x => x.Name)
	.ToList()
	.Dump();	
Customers
	.Select(x => new {
		CustomerID = x.CustomerID,
		Name = x.FirstName + " " + x.LastName,
		AveragePurchases = x.Invoices.Average(i => i.TotalAmount) > 0 ?
			x.Invoices.Average(i => i.TotalAmount) : 0
	})
	.Where(x => x.AveragePurchases > 0)
	.OrderBy(x => x.Name)
	.ToList()
	.Dump();