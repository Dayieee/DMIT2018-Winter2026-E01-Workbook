<Query Kind="Statements">
  <Connection>
    <ID>34b22ad5-e474-47ae-9e8a-09e88e499133</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DAVE\SQLEXPRESS</Server>
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

Employees
.Where(x => x.HireDate>= (new DateTime(2002, 01, 01)))
.Select(x => x)
.Dump();


Invoices 
.Where(x => x.InvoiceDate >= (new DateTime(2023, 01, 01)) && 
x.BillingCountry == "Canada")
.Select(x => x)
.Dump();

Invoices 
.Where(x => x.Total >= 5 && x.Total <=10)
.Select(x => x)
.Dump();

Customers
.Where(x => x.FirstName.Contains("l"))
.Select(x => x)
.Dump();

//Order By Query
Albums 
.OrderBy(x => x.Title)
.ThenByDescending(x => x.ReleaseYear)
.Dump();

Albums
.OrderBy(x => x.Title)
.ThenByDescending(x => x.ReleaseYear)
.Select(x => x)
.Dump();

