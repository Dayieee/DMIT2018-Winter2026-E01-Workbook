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
//Question 1: Group By (Simple)
ProductSubcategories
	//.AsEnumerable()
	//.GroupBy(x => x.ProductCategory.ProductCategoryName)
	//.OrderBy(g => g.Key)
	.GroupBy(x => new {
		x.ProductCategory.ProductCategoryName
	})
	.OrderBy(g => g.Key.ProductCategoryName)
	.Select(g => new {
		CategoryName = g.Key.ProductCategoryName,
		ProductySubcategories = g.Select(x => new {
			SubcategoryName = x.ProductSubcategoryName
		})
		.OrderBy(x => x.SubcategoryName)
		.ToList()
	})
	.ToList()
	.Dump();
	
//Question 2: Group By (Complex)
InvoiceLines
	.AsEnumerable()
	.GroupBy(x => new {
		x.Product.ProductSubcategory.ProductCategory.ProductCategoryName,
		x.Product.ProductSubcategory.ProductSubcategoryName
	})
	.Select(g => new {
		CategoryName = g.Key.ProductCategoryName,
		SubcategoryName = g.Key.ProductSubcategoryName,
		Invoices = g.Select(x => new {
			InvoiceID = x.InvoiceID,
			Product = x.Product.ProductName,
			Amount = x.SalesQuantity
		})
		.OrderBy(x => x.Product)
	})
	.OrderBy(x => x.CategoryName)
	.ThenBy(x => x.SubcategoryName)
	.Dump();