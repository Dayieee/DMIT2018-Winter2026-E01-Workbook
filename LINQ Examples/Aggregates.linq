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

//In-Class Review
//Question 1: Count()
Customers
	.Where(x => x.TotalChildren > 0)
	.Count()
	.Dump();
	
//Question 3: Sum()
Products
	.Select(x => new {
		Name = x.ProductName,
		TotalOnHand = x.Inventories.Count() == 0 ? 0 :
						x.Inventories.Sum(i => i.OnHandQuantity)
	})
	.OrderBy(x => x.Name)
	.Dump();
	
//Question 5: Min()
ProductSubcategories
	.Select(x => new {
		Category = x.ProductCategory.ProductCategoryName,
		SubCategory = x.ProductSubcategoryName,
		LowestCost = x.Products.Min(p => p.UnitCost),
		LowestPrice = x.Products.Min(p => p.UnitPrice)
	})
	.Where(x => x.LowestCost != null && x.LowestPrice != null)
	.OrderBy(x => x.Category)
	.Dump();
	
//Question 7: Max()
ProductSubcategories
	.Select(x => new {
		Category = x.ProductCategory.ProductCategoryName,
		SubCategory = x.ProductSubcategoryName,
		LowestCost = x.Products.Min(p => p.UnitCost),
		LowestPrice = x.Products.Min(p => p.UnitPrice),
		MaxCost = x.Products.Max(p => p.UnitCost),
		MaxPrice = x.Products.Max(p => p.UnitPrice)
	})
	.Where(x => x.LowestCost != null && x.LowestPrice != null)
	.OrderBy(x => x.Category)
	.Dump();
	
//Question 9: Average()
Invoices
	.Select(x => new {
		InvoiceNo = x.InvoiceID,
		InvoiceDate = x.DateKey.ToString("M'/'d'/'yyyy"),
		AverageQty = x.InvoiceLines.Average(i => i.SalesQuantity),
		AverageQtyRound = Math.Round(x.InvoiceLines.Average(i => i.SalesQuantity), 0)
	})
	.Dump();