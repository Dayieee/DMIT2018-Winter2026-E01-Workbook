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

//Take-Home Practice (4 Questions) - WHERE Clauses

//Question 1: Single Where Clause, All Fields
//Context: "We need to review all invoices created after November 2023 to ensure they were processed correctly."
//Question: "How would you filter the Invoice table to retrieve these invoices?"
//Invoices
//    .Where(x => x.DateKey > new DateTime(2023, 11, 30))
//    .Select(x => x)
//    .Dump();
Invoices
    .Where(x => DateOnly.FromDateTime(x.DateKey) > (new DateOnly(2023, 11, 30)))
    .Select(x => x)
    .Dump();

//Question 2: Single Where Clause, All Fields
//Context: "Our regional analysis team needs to focus on all sales territories located in Canada for a new market expansion project."
//Question: "How would you filter the Geography table to retrieve all records where the country is Canada?"
Geographies
    .Where(x => x.RegionCountryName == "Canada")
    .Select(x => x)
    .Dump();
//Geographies
//    .Where(x => x.RegionCountryName.ToUpper().Contains("CANADA"))
//    .Select(x => x)
//    .Dump();	
//Geographies
//    .Where(x => x.RegionCountryName.ToLower().Contains("canada"))
//    .Select(x => x)
//    .Dump();	

//Question 3: Multiple Field Selection
//Context: "After reviewing the previous data output, we noticed records with GeographyType labeled as 'Country/Region.' For our detailed analysis,
//we only want to focus on cities located in Ontario, Canada."
//Question: "How would you filter the Geography table to retrieve records where the Type is 'City' and the Province Name is 'Ontario'?"
Geographies
    .Where(x => x.RegionCountryName.ToUpper() == "CANADA" &&
			x.GeographyType.ToUpper() == "CITY" &&
			x.StateProvinceName.ToUpper() == "ONTARIO")
    .Select(x => x)
    .Dump();
//Geographies
//    .Where(x => x.RegionCountryName.ToUpper().Contains("CANADA") &&
//			x.GeographyType.ToUpper().Contains("CITY") &&
//			x.StateProvinceName.ToUpper().Contains("ONTARIO"))
//    .Select(x => x)
//    .Dump();
	
//Question 4: Filtering using Contain
//Context: "There has been some confusion with store names that include the term 'No.' in them, which might indicate store numbers or branches.
//We need to identify all stores with 'No.' in their names to review their details and address any inconsistencies."
//Question: "How would you filter the Store table to retrieve all records where the StoreName contains 'No.'?"
Stores
    .Where(x => x.StoreName.ToUpper().Contains("NO."))
    .Select(x => x)
    .Dump();

//Another contains example 
Stores
	.Where(x => x.StoreName.Contains("Greeley"))
	.Dump(); 