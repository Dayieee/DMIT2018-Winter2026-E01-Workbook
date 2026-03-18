<Query Kind="Expression" />

void Main()
{
    GetInventoryOnHandByCity("Seattle", 200).Dump();
}

public List<StoreSummaryView> GetInventoryOnHandByCity(string cityName, int minOnHand)
{
    return Stores
        .Where(x => x.Geography.CityName.Contains(cityName))
        .Select(x => new StoreSummaryView
        {
            StoreName = x.StoreName,
            City = x.Geography.CityName,
            Inventory = x.Inventories
                .Where(i => i.OnHandQuantity >= minOnHand)
                .Select(i => new InventoryView
                {
                    Name = i.Product.ProductName,
                    Price = i.Product.UnitPrice,
                    Cost = i.Product.UnitCost,
                    Margin = i.Product.UnitPrice - i.Product.UnitCost,
                    OnHand = i.OnHandQuantity
                })
                .OrderBy(i => i.Name)
                .ToList()
        })
        .ToList();
}

public class StoreSummaryView
{
    public string StoreName { get; set; }
    public string City { get; set; }
    public List<> Inventory { get; set; }
}

public class InventoryView
{
    public string Name { get; set; }
    public decimal? Price { get; set; }
    public decimal? Cost { get; set; }
    public decimal? Margin { get; set; }
    public int OnHand { get; set; }
}