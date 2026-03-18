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

void Main()
{
	GetProductCategories().Dump();
}

// You can define other methods, fields, classes and namespaces here
public List<ProductCategorySummaryView> GetProductCategories()
{
	return ProductCategories
			.Select(x => new ProductCategorySummaryView {
				ProductCategoryName = x.ProductCategoryName,
				SubCategories = x.ProductSubcategories
									.Select(x => new ProductSubcategorySummaryView {
										SubCategoryName = x.ProductSubcategoryName,
										Description = x.ProductSubcategoryDescription
									})
									.ToList()
			})
			.ToList();
}

public class ProductCategorySummaryView
{
	public string ProductCategoryName { get; set; }
	public List<ProductSubcategorySummaryView> SubCategories { get; set; } 
}

public class ProductSubcategorySummaryView
{
	public string SubCategoryName { get; set; }
	public string Description { get; set; }
}