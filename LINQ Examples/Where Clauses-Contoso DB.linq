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

//Question 1: Single Where Clause, All Fields
//Context: "We need to identify all employees hired after January 1, 2022, 
//to ensure they are included in our new training program."
//Question: "How would you filter the Employee table to retrieve these employees?"
Employees
	.Where(x => x.HireDate > new DateTime(2022, 1, 1))
	.Select(x => x)
	.Dump();
	
//Question 2: Multiple Where Clause, All Fields
//Context: "Our inventory team wants to find all products that have been available for sale
//since July 1, 2019, to ensure they are properly stocked."
//Question: "How would you filter the Product table to retrieve these products?"
//Products
//	.Where(x => x.AvailableForSaleDate.HasValue)
//	.Select(x => x)
//	.Where(x => x.AvailableForSaleDate >= new DateTime(2019, 7, 1))
//	.Dump();
//var products = Products
//				.Where(x => x.AvailableForSaleDate.HasValue)
//				.Select(x => x);
////var products = Products
////				.Where(x => x.AvailableForSaleDate != null)
////				.Select(x => x);
//products
//	.Where(x => x.AvailableForSaleDate >= (new DateTime(2019, 7, 1)))
//	.Select(x => x)
//	.Dump();
	
//Products
//	.Where(x => x.AvailableForSaleDate == null)
//	.Dump();
	
//Products
//	.Where(x => x.AvailableForSaleDate >= new DateTime(2019, 7, 1))
//	.Select(x => x)
//	.Dump();
	

//Question 3: Multiple Where Clauses
//Context: "To update our customer database, we need to pull the email addresses of
//all customers with a yearly income between $60,000 & $61,000."
//Question: "How would you filter the Customer table and retrieve only the email addresses
//of these customers?"
Customers
	.Where(x => x.YearlyIncome >= 60000 && x.YearlyIncome <= 61000)
	.Select(x => x.EmailAddress)
	.Dump();

//Validation from the above
//Customers
//	.Where(x => x.YearlyIncome >= 60000 && x.YearlyIncome <= 61000)
//	.Select(x => new {
//		EmailAdd = x.EmailAddress,
//		YearlyIncome = x.YearlyIncome
//	})
//	.OrderBy(x => x.YearlyIncome)
//	.Dump();

//Question 4: Filtering using Contain
//Context: "The marketing department needs a list of all promotions focused on
//North America to prepare for the upcoming sale."
//Question: "How would you filter the Promotion table to retrieve the promotion information?"
Promotions
	.Where(x => x.PromotionName.ToUpper().Contains("NORTH AMERICA"))
	.Select(x => x)
	.Dump();

Promotions
	.Where(x => x.PromotionName.ToLower().Contains("north america"))
	.Select(x => x)
	.Dump();