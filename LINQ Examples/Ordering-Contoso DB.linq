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

//Question 1: Single Where Clause, Ordered by Last name
//Context: "We need to identify all employees hired after January 1, 2022,
//to ensure they are included in our new training program."
//Question: "How would you filter the Employee table to retrieve these employees,
//ordered by last name?"
Employees
	.Where(x => x.HireDate > new DateTime(2022, 1, 1))
	.Select(x => x)
	.OrderBy(x => x.LastName)
	.Dump();
	
//Question 3: Multiple Where Clauses, Ordered by Email Address
//Context: "To update our customer database, we need to pull the email addresses of 
//all customers with a yearly income between $60,000 and $61,000."
//NOTE: Order by must follow the where clause but before the select.
//Question: "How would you filter the Customer table and retrieve only
//the email addresses of these customers, ordered by email address?"
Customers
	.Where(x => x.YearlyIncome >= 60000 && x.YearlyIncome <= 61000)
	.OrderBy(x => x.EmailAddress)
	.Select(x => x.EmailAddress)
	.Dump();
	
//Question 4: Filtering using Contains, Ordered by Promotion Name and Start Date
//Context: "The marketing department needs a list of all promotions focused on North America 
//to prepare for the upcoming sale."
//Question: "How would you filter the Promotion table to retrieve the promotion information, 
//ordered by promotion name?"
Promotions
	.Where(x => x.PromotionName.ToUpper().Contains("NORTH AMERICA"))
	.Select(x => x)
	.OrderBy(x => x.PromotionName)
	.ThenBy(x => x.StartDate)
	.Dump();