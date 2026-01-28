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
//Return all available fields and order the results by the customer’s last name.
Employees
	.OrderBy(x => x.LastName)
	.Where(x => x.HireDate > (new DateTime(2003, 01, 01)))
	.Select(x => x)
	.Dump();
	
Employees
	.Where(x => x.HireDate > (new DateTime(2003, 01, 01)))
	.Select(x => new
	{
		FirstName = x.FirstName,
		Last = x.LastName
	})
	.OrderBy(x => x.Last)	
	.Dump();

//Question 2: Multiple Where Clauses, All Fields
//Context: The finance team needs to generate a report of all customer invoices issued from January 1, 2003
//through the present date. The report should include only invoices billed to customers located in Canada.
//Question: How would you apply filters to the Invoice table to retrieve all invoices dated on or after
//January 1, 2003 that have a billing country of Canada?
//Ensure that the results are ordered from the most recent invoice to the oldest.
Invoices
	.Where(x => x.InvoiceDate >= (new DateTime(2023, 01, 01)) &&
			x.BillingCountry == "Canada")
	.Select(x => x)
	.OrderByDescending(x => x.InvoiceDate)
	.Dump();

//Question 3: Multiple Where Clauses, All Fields
Customers
	.OrderBy(x => x.SupportRepId)
	.ThenByDescending(x => x.Country)
	.ThenBy(x => x.State)
	.Select(x => x)
	.Dump();
	
