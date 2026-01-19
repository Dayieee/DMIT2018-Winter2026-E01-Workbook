<Query Kind="Statements">
  <Connection>
    <ID>6b821400-0acf-4186-ba5f-2174b2f7f2fe</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(local)</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <EncryptTraffic>true</EncryptTraffic>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook-2025</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>

//Question 1: Single Where Clause, All Fields
//Context: We need to identify all employees hired after January 1, 2003,
//to ensure they are included in our new training program.
//Question: How would you filter the Employee table to retrieve these employees?
Employees
	.Where(x => x.HireDate > (new DateTime(2003, 01, 01)))
	.Select(x => x)
	.Dump();

//Question 2: Multiple Where Clauses, All Fields
//Context: The finance team needs to generate a report of all customer invoices issued from January 1, 2003
//through the present date. The report should include only invoices billed to customers located in Canada.
//Question: How would you apply filters to the Invoice table to retrieve all invoices dated on or after
//January 1, 2003 that have a billing country of Canada?
Invoices
	.Where(x => x.InvoiceDate >= (new DateTime(2023, 01, 01)) &&
			x.BillingCountry == "Canada")
	.Select(x => x)
	.Dump();

//Question 3: Multiple Where Clauses, All Fields
Invoices
	.Where(x => x.Total >= 5 && x.Total <= 10)
	.Select(x => x)
	.Dump();

//Question 4: Filtering using Contain
Customers
	.Where(x => x.FirstName.Contains("l"))
	.Select(x => x)
	.Dump();
	
