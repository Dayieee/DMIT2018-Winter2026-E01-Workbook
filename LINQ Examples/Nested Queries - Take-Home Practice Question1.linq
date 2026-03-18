<Query Kind="Expression" />

void Main()
{
    GetManagerAndSubordinates().Dump();
}

public List<ManagerView> GetManagerAndSubordinates()
{
    return Employees
        .Select(x => new ManagerView
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
            Position = x.Title,
            Employees = x.Children
                .OrderBy(c => c.LastName)
                .Select(c => new SubordinateView
                {
                    Name = $"{c.FirstName} {c.LastName}",
                    Title = c.Title
                }).ToList()
        })
        .OrderBy(x => x.LastName)
        .ToList();
}

public class ManagerView
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public List<> Employees { get; set; }
}

public class SubordinateView
{
    public string Name { get; set; }
    public string Title { get; set; }
}