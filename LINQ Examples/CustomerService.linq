<Query Kind="Program">
  <Connection>
    <ID>32a2c8ac-1482-45d7-bc6f-9bbdfbaac42a</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>(local)</Server>
    <Database>OLTP-DMIT2018</Database>
    <DisplayName>OLTP-DMIT2018</DisplayName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
  <Reference Relative="..\..\..\..\..\..\..\AppData\Local\Temp\LINQPad9\Exported Assemblies\AssemblySet3\BYSResults.dll">&lt;TempFiles&gt;\LINQPad9\Exported Assemblies\AssemblySet3\BYSResults.dll</Reference>
</Query>

// 	Lightweight result types for explicit success/failure 
//	 handling in .NET applications.
using BYSResults;

// —————— PART 1: Main → UI ——————
//	Driver is responsible for orchestrating the flow by calling 
//	various methods and classes that contain the actual business logic 
//	or data processing operations.
void Main()
{
	CodeBehind codeBehind = new CodeBehind(this); // “this” is LINQPad’s auto Context

	int customerId = 0;

	codeBehind.GetCustomer(customerId);
	//codeBehind.ErrorDetails.Dump("Customer ID must be greater than zero");
	
	customerId = 1000000;
	codeBehind.GetCustomer(customerId);
	//codeBehind.ErrorDetails.Dump($"Customer was not found for ID {customerId}");

	customerId = 1;
	codeBehind.GetCustomer(customerId);
	//codeBehind.Customer.Dump("Pass - Valid Customer ID");
	
	codeBehind.AddEditCustomer(null);
	//codeBehind.ErrorDetails.Dump("Customer is null");
	
	CustomerEditView customer = new();
	
	codeBehind.AddEditCustomer(customer);
	//codeBehind.ErrorDetails.Dump("Missing required fields");
	
	customer = codeBehind.Customer;
	customer.CustomerID = 0;
	codeBehind.AddEditCustomer(customer);
	//codeBehind.ErrorDetails.Dump("Duplicated customer");
	
	//customer = new CustomerEditView()
	customer = new()
	{
		FirstName = "Test First Name",
		LastName = "Test Last Name",
		Address1 = "Test Address 1",
		Address2 = "Test Address 2",
		City = "Test City",
		ProvStateID = Lookups.Where(l => l.Name == "Alberta")
						.Select(l => l.LookupID).FirstOrDefault(),
		//ProvStateID = Lookups.FirstOrDefault(l => l.Name == "Alberta").LookupID,
		CountryID = Lookups.Where(l => l.Name == "Canada")
						.Select(l => l.LookupID).FirstOrDefault(),
		PostalCode = "T6W5R2",
		Phone = "1234567890",
		Email = "test@gmail.com",
		StatusID = Lookups.Where(l => l.Name == "Silver")
						.Select(l => l.LookupID).FirstOrDefault(),
		RemoveFromViewFlag = false
	};
	//Customers.OrderByDescending(c => c.CustomerID).Take(3).Dump("Before Adding a New Customer Record");
	
	//codeBehind.AddEditCustomer(customer);
	//codeBehind.Customer.Dump("New Customer");
	
	//Customers.OrderByDescending(c => c.CustomerID).Take(2).Dump("After Adding a New Customer Record");
	codeBehind.GetCustomer(1034);
	customer = codeBehind.Customer;
	customer.Address1 = "Address 1";
	customer.Address2 = "Address 2";
	
	//Customers.OrderByDescending(c => c.CustomerID).Take(2).Dump("Before Editing a New Customer Record");
	
	//codeBehind.AddEditCustomer(customer);
	//codeBehind.Customer.Dump("Edit Customer");
	
	//Customers.OrderByDescending(c => c.CustomerID).Take(2).Dump("After Editing a New Customer Record");
}

// ———— PART 2: Code Behind → Code Behind Method ————
// This region contains methods used to test the functionality
// of the application's business logic and ensure correctness.
// NOTE: This class functions as the code-behind for your Blazor pages
#region Code Behind Methods
public class CodeBehind(TypedDataContext context)
{
	#region Supporting Members (Do not modify)
	// exposes the collected error details
	public List<string> ErrorDetails => errorDetails;

	// Mock injection of the service into our code-behind.
	// You will need to refactor this for proper dependency injection.
	// NOTE: The TypedDataContext must be passed in.
	private readonly Library YourService = new Library(context);
	#endregion

	#region Fields from Blazor Page Code-Behind
	// feedback message to display to the user.
	private string feedbackMessage = string.Empty;
	// collected error details.
	private List<string> errorDetails = new();
	// general error message.
	private string errorMessage = string.Empty;
	#endregion

	public CustomerEditView Customer = default!;
	
	public void GetCustomer(int customerId)
	{
		errorDetails.Clear();
		errorMessage = string.Empty;
		feedbackMessage = string.Empty;
		
		try
		{
			Result<CustomerEditView> result = YourService.GetCustomer(customerId);
			
			if (result.IsSuccess)
			{
				Customer = result.Value;
			}
			else
			{
				errorDetails = GetErrorMessages(result.Errors.ToList());
			}
		}
		catch (Exception ex)
		{
			errorMessage = ex.Message;
		}
	}
	
	public void AddEditCustomer(CustomerEditView editCustomer)
	{
		errorDetails.Clear();
		errorMessage = string.Empty;
		feedbackMessage = string.Empty;
		
		try
		{
			Result<CustomerEditView> result = YourService.AddEditCustomer(editCustomer);
			
			if (result.IsSuccess)
			{
				Customer = result.Value;
			}
			else
			{
				errorDetails = GetErrorMessages(result.Errors.ToList());
			}
		}
		catch (Exception ex)
		{
			errorMessage = ex.Message;
		}
	}
}
#endregion

// ———— PART 3: Database Interaction Method → Service Library Method ————
//	This region contains support methods for testing
#region Methods
public class Library
{
	#region Data Context Setup
	// The LINQPad auto-generated TypedDataContext instance used to query and manipulate data.
	private readonly TypedDataContext _oltpDMIT2018Context;

	// The TypedDataContext provided by LINQPad for database access.
	// Store the injected context for use in library methods
	// NOTE:  This constructor is simular to the constuctor in your service
	public Library(TypedDataContext context)
	{
		_oltpDMIT2018Context = context
					?? throw new ArgumentNullException(nameof(context));
	}
	#endregion

	public Result<CustomerEditView> GetCustomer(int customerId)
	{
		//var result = new Result<List<CustomerEditView>>();
		Result<CustomerEditView> result = new();
	
		#region Business Rules
		if (customerId <= 0)
		{
			result.AddError(new Error("Missing Information",
				"Please provide a valid Customer ID"));
			
			return result;
		}
		#endregion
		
		var customer = _oltpDMIT2018Context.Customers
						.Where(c => c.CustomerID == customerId
								&& !c.RemoveFromViewFlag)
						.Select(c => new CustomerEditView
						{
							CustomerID = c.CustomerID,
							FirstName = c.FirstName,
							LastName = c.LastName,
							Address1 = c.Address1,
							Address2 = c.Address2,
							City = c.City,
							ProvStateID = c.ProvStateID,
							CountryID = c.CountryID,
							PostalCode = c.PostalCode,
							Phone = c.Phone,
							Email = c.Email,
							StatusID = c.StatusID,
							RemoveFromViewFlag = c.RemoveFromViewFlag
						})
						.FirstOrDefault();
						
		if (customer == null)
		{
			result.AddError(new Error("No Customer",
				"No Customer were found"));
			
			return result;
		}
		
		return result.WithValue(customer);
	}
	
	public Result<CustomerEditView> AddEditCustomer(CustomerEditView editCustomer)
	{
		Result<CustomerEditView> result = new();
		
		#region Business Rules
		if (editCustomer == null)
		{
			result.AddError(new Error("Missing Customer",
				"No customer was supply"));
			
			return result;
		}
		
		if (string.IsNullOrWhiteSpace(editCustomer.FirstName))
		{
			result.AddError(new Error("Missing Information",
				"First Name is required"));
		}
		
		if (string.IsNullOrWhiteSpace(editCustomer.LastName))
		{
			result.AddError(new Error("Missing Information",
				"Last Name is required"));
		}
		
		if (string.IsNullOrWhiteSpace(editCustomer.Phone))
		{
			result.AddError(new Error("Missing Information",
				"Phone is required"));
		}
		
		if (string.IsNullOrWhiteSpace(editCustomer.Email))
		{
			result.AddError(new Error("Missing Information",
				"Email is required"));
		}
		
		if (editCustomer.CustomerID == 0)
		{
			//bool customerExist = _oltpDMIT2018Context.Customers
			//						.Any(x => x.FirstName.ToUpper() == editCustomer.FirstName.ToUpper() &&
			//						x.LastName.ToUpper() == editCustomer.LastName.ToUpper() &&
			//						x.Phone == editCustomer.Phone);
			bool customerExist = _oltpDMIT2018Context.Customers
										.Any(x =>
											(!string.IsNullOrWhiteSpace(editCustomer.FirstName)
												&& x.FirstName.ToUpper() == editCustomer.FirstName.ToUpper()) &&
											(!string.IsNullOrWhiteSpace(editCustomer.LastName)
												&& x.LastName.ToUpper() == editCustomer.LastName.ToUpper()) &&
											(!string.IsNullOrWhiteSpace(editCustomer.Phone)
												&& x.Phone == editCustomer.Phone)
										);
						
			if (customerExist)
			{
				result.AddError(new Error("Existing Customer Data",
				"Customer already exist in the database and cannot be enter again"));
			}
		}
		
		if (result.IsFailure)
		{
			return result;
		}
		#endregion
		
		Customer customer = _oltpDMIT2018Context.Customers
								.Where(x => x.CustomerID == editCustomer.CustomerID)
								.Select(x => x)
								.FirstOrDefault();
		
		if (customer == null)
		{
			//customer = new Customer();
			customer = new();
		}
		
		customer.FirstName = editCustomer.FirstName;
		customer.LastName = editCustomer.LastName;
		customer.Address1 = editCustomer.Address1;
		customer.Address2 = editCustomer.Address2;
		customer.City = editCustomer.City;
		customer.ProvStateID = editCustomer.ProvStateID;
		customer.CountryID = editCustomer.CountryID;
		customer.PostalCode = editCustomer.PostalCode;
		customer.Phone = editCustomer.Phone;
		customer.Email = editCustomer.Email;
		customer.StatusID = editCustomer.StatusID;
		customer.RemoveFromViewFlag = editCustomer.RemoveFromViewFlag;
		
		if (customer.CustomerID == 0)
		{
			_oltpDMIT2018Context.Customers.Add(customer);
		}
		else
		{
			_oltpDMIT2018Context.Customers.Update(customer);
		}
		
		try
		{
			_oltpDMIT2018Context.SaveChanges();
		}
		catch (Exception ex)
		{
			_oltpDMIT2018Context.ChangeTracker.Clear();
			result.AddError(new Error("Error Saving Changes",
				ex.InnerException.Message));
				
			return result;
		}
		
		return GetCustomer(customer.CustomerID);
	}
}
#endregion

// ———— PART 4: View Models → Service Library View Model ————
//	This region includes the view models used to 
//	represent and structure data for the UI.
#region View Models
public class CustomerEditView
{
	public int CustomerID { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Address1 { get; set; }
	public string Address2 { get; set; }
	public string City { get; set; }
	public int ProvStateID { get; set; }
	public int CountryID { get; set; }
	public string PostalCode { get; set; }
	public string Phone { get; set; }
	public string Email { get; set; }
	public int StatusID { get; set; }
	public bool RemoveFromViewFlag { get; set; }
}
#endregion

//	This region includes support methods
#region Support Method
// Converts a list of error objects into their string representations.
public static List<string> GetErrorMessages(List<Error> errorMessage)
{
	// Initialize a new list to hold the extracted error messages
	List<string> errorList = new();

	// Iterate over each Error object in the incoming list
	foreach (var error in errorMessage)
	{
		// Convert the current Error to its string form and add it to errorList
		errorList.Add(error.ToString());
	}

	// Return the populated list of error message strings
	return errorList;
}
#endregion