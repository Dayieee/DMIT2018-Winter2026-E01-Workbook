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
//Question 1: Where Clause with OrderBy and Anonymous Data Set
Customers
	.Where(x => x.Occupation == "Clerical" &&
			x.MaritalStatus == "S" &&
			x.YearlyIncome < 51000)
	.OrderBy(x => x.LastName)
	.Select(x => new {
		// String Concatenation with "+"
		NameConcat = x.FirstName + " " + x.LastName,
		// String Interpolation
		Name = $"{x.FirstName} {x.LastName}",
		Income = x.YearlyIncome,
		Childrens = x.TotalChildren
	})			
	.Dump();
	
//Question 2: Where Clause with OrderBy and Anonymous Data Set
Customers
	.Where(x => x.Occupation == "Clerical" &&
			x.MaritalStatus == "S" &&
			x.YearlyIncome < 51000)
	.Select(x => new {
		Name = $"{x.FirstName} {x.LastName}",
		Income = x.YearlyIncome,
		Childrens = x.TotalChildren,
		City = x.Geography.CityName,
		Country = x.Geography.RegionCountryName
	})
	.OrderBy(x => x.Country)
	.ThenBy(x => x.City)
	.Dump();
	
//Question 3: Where Clause with OrderBy and Anonymous Data Set
InvoiceLines
	.Where(x => x.ReturnQuantity > 6 &&
			x.Invoice.DateKey.Year == 2023 &&
			x.Invoice.DateKey.Month == 2)
	.OrderBy(x => x.Invoice.DateKey)
	.Select(x => new {
		InvoiceDate = x.Invoice.DateKey.ToString("M'/'d'/'yyyy"),
		InvoiceDateDTString = x.Invoice.DateKey.Date.ToString("M'/'d'/'yyyy"),
		InvoiceDateOnly = new DateOnly(x.Invoice.DateKey.Year, x.Invoice.DateKey.Month, x.Invoice.DateKey.Day).ToString("M'/'d'/'yyyy"),
		InvoiceDateOnlyFrDT = DateOnly.FromDateTime(x.Invoice.DateKey).ToString("M'/'d'/'yyyy"),
		InvoiceDateConCat = x.Invoice.DateKey.Month + "/" +
			x.Invoice.DateKey.Day + "/" +
			x.Invoice.DateKey.Year,
		Customer = $"{x.Invoice.Customer.FirstName} {x.Invoice.Customer.LastName}",
		invoiceID = x.InvoiceID,
		Store = x.Invoice.Store.StoreName,
		ProductID = x.ProductID,
		Name = x.Product.ProductName,
		Qty = x.ReturnQuantity,
		TotalReturn = x.ReturnAmount
	})
	.OrderBy(x => x.invoiceID)
	.Dump();