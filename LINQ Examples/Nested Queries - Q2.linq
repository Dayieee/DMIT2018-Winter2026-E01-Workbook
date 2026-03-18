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
	GetInvoicesWithDetails("Torres").Dump();
}

// You can define other methods, fields, classes and namespaces here
public List<InvoiceView> GetInvoicesWithDetails(string lastName)
{
	return Invoices
			.Where(x => x.Customer.LastName == lastName)
			.Select(x => new InvoiceView {
				InvoiceNo = x.InvoiceID,
				InvoiceDate = x.DateKey.ToShortDateString(),
				Customer = $"{x.Customer.FirstName} {x.Customer.LastName}",
				Amount = x.TotalAmount,
				Details = x.InvoiceLines
							.Select(il => new InvoiceLineView {
								LineReference = il.InvoiceLineID,
								ProductName = il.Product.ProductName,
								Qty = il.SalesQuantity > 0 ? il.SalesQuantity :
										il.ReturnQuantity * -1,
								Price = il.SalesQuantity > 0 ? il.UnitPrice :
										il.UnitPrice * -1,
								Discount = il.SalesQuantity > 0 ? il.DiscountAmount :
										il.DiscountAmount * -1,
								ExtPrice = il.SalesQuantity > 0 ? il.SalesAmount :
										il.ReturnAmount * -1
							})
							.OrderBy(il => il.LineReference)
							.ToList()
			})
			.ToList();
}

public class InvoiceView
{
	public int InvoiceNo { get; set; }
	public string InvoiceDate { get; set; }
	public string Customer { get; set; }
	public decimal Amount { get; set; }
	public List<InvoiceLineView> Details { get; set; } 
}

public class InvoiceLineView
{
	public int LineReference { get; set; }
	public string ProductName { get; set; }
	public int Qty { get; set; }
	public decimal? Price { get; set; }
	public decimal? Discount { get; set; }
	public decimal? ExtPrice { get; set; }
}