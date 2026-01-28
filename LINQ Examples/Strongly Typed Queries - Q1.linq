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

//Question 1: Strongly Typed Queries
//Context: "The HR department at Contoso Corporation is conducting a customized salary
//review, allowing them to target specific groups of employees based on both their last
//name and their hourly wage. This approach helps HR quickly identify employees who might
//be underpaid. For this task, they need to generate a list of employees whose last names
//match a given search term and whose base rate is below a specified threshold. The HR team
//will provide these parameters to ensure flexibility in their search. The list should
//include the employee's full name, department, and an income category indicating whether
//their salary requires a review. The results should be ordered alphabetically by last name."
//Objective: "Create a method that retrieves employee records based on a search for last 
//names and a base rate threshold. The method should take two parameters—lastName and
//baseRate—and return a strongly typed list of EmployeeView objects, containing the
//employee's full name, department, and income category, ordered by last name."
void Main()
{
	GetEmployeeReview("al", 30).Dump();
}

// You can define other methods, fields, classes and namespaces here
public List<EmployeeView> GetEmployeeReview(string partialLastName, decimal baseRate)
{
	return Employees
			.Where(x => x.LastName.Contains(partialLastName))
			.OrderBy(x => x.LastName)
			.Select(x => new EmployeeView {
				FullName = $"{x.FirstName} {x.LastName}",
				Department = x.DepartmentName,
				IncomeCategory = x.BaseRate < baseRate ? "Required Review" : "No Review Required"
			})
			.ToList();
}

public class EmployeeView
{
	public string FullName { get;set; }
	public string Department { get;set; }
	public string IncomeCategory { get;set; }
}
