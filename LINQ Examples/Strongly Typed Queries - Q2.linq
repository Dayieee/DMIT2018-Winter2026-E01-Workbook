<Query Kind="Program">
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

//Question 2: Strongly Typed Queries
//Context: "The production team at Contoso Corporation needs to review their product line
//to determine which products require additional color processing. The team is particularly
//interested in products within specific categories, which they will specify as needed.
//For this task, the production team will provide a category name to search for products
//within that category. They need to identify whether each product's color requires
//additional processing, with black and white colors not needing further processing.
//The results should be organized by the product's style name to facilitate the review
//process."
//Objective: "Create a method that retrieves product records based ona category name search.
//The method should take a categoryName parameter and return a strongly typed list of
//ProductColorProcessView objects, containing the product name, color, and whether
//additional color processing is needed, ordered by the product's style name."
void Main()
{
	GetProductColorProcess("Music").Dump();
}

// You can define other methods, fields, classes and namespaces here
public List<ProductColorProcessView> GetProductColorProcess(string categoryName)
{
	return Products
			.Where(x => x.ProductSubcategory.ProductCategory.ProductCategoryName.Contains(categoryName))
			.OrderBy(x => x.StyleName)
			.Select(x => new ProductColorProcessView {
				ProductName = x.ProductName,
				Color = x.ColorName,
				ColorProcessNeeded = x.ColorName == "Black" || x.ColorName == "White" ? "No" : "Yes"
			})
			.ToList();
}

public class ProductColorProcessView 
{
	public string ProductName { get;set; }
	public string Color { get;set; }
	public string ColorProcessNeeded { get;set; }		
}