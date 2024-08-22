using System.Transactions;

List<Product> products = new List<Product>()
{
    new Product()
    { 
        Name = "Football", 
        Price = 15.00M, 
        SoldOnDate = null,
        StockDate = new DateTime(2022, 10, 20),
        ManufactureYear = 2010,
        Condition = 4.2
    },
    new Product() 
    { 
        Name = "Hockey Stick", 
        Price = 12.50M, 
        SoldOnDate = null,
        StockDate = new DateTime(2023, 5, 12),
        ManufactureYear = 2022,
        Condition = 3.8
    },
    new Product()
    {
        Name = "Basketball",
        Price = 10.75M,
        SoldOnDate = null,
        StockDate = new DateTime(2024, 8, 13),
        ManufactureYear = 2023,
        Condition = 4.5
    },
    new Product()
    {
        Name = "Cleats",
        Price = 20.99M,
        SoldOnDate = new DateTime(2024, 8, 20),
        StockDate = new DateTime(2024, 7, 30),
        ManufactureYear = 2021,
        Condition = 3.4
    },
    new Product()
    {
        Name = "Tennis Racket",
        Price = 45.00M,
        SoldOnDate = new DateTime(2024, 8, 10),
        StockDate = new DateTime(2024, 5, 5),
        ManufactureYear = 2023,
        Condition = 4.0
    },
    new Product()
    {
        Name = "Baseball Glove",
        Price = 25.00M,
        SoldOnDate = new DateTime(2024, 8, 15),
        StockDate = new DateTime(2024, 3, 15),
        ManufactureYear = 2022,
        Condition = 3.9
    },
    new Product()
    {
        Name = "Golf Club",
        Price = 120.00M,
        SoldOnDate = new DateTime(2024, 8, 5),
        StockDate = new DateTime(2023, 11, 10),
        ManufactureYear = 2023,
        Condition = 4.7
    },
    new Product()
    {
        Name = "Soccer Ball",
        Price = 18.00M,
        SoldOnDate = new DateTime(2024, 8, 18),
        StockDate = new DateTime(2024, 2, 22),
        ManufactureYear = 2024,
        Condition = 4.3
    }
};
string greeting = @"Welcome to Thrown For a Loop
Your one-stop shop for used sporting equipment";
Console.WriteLine(greeting);

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Choose an option:
0. Exit
1. View All Products
2. View Product Details
3. View Latest Products
4. Monthly Sales Report");
    choice = Console.ReadLine();
    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1")
    {
        ListProducts();
    }
    else if (choice == "2")
    {
        ViewProductDetails();
    }
    else if (choice == "3")
    {
        ViewLatestProducts();
    }
    else if (choice == "4")
    {
        MonthlySalesReport();
    }
}


void ViewProductDetails()
{
    ListProducts();

    Product chosenProduct = null;

    while (chosenProduct == null)
    {
        Console.WriteLine("Please enter a product number: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenProduct = products[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }

    Console.WriteLine(@$"You chose: 
    {chosenProduct.Name}, which costs {chosenProduct.Price} dollars.
    It is {DateTime.Now.Year - chosenProduct.ManufactureYear} years old.
    It {(chosenProduct.SoldOnDate == null ? $"has been in stock for {chosenProduct.TimeInStock.Days} days." : "is not available.")}
    Condition rating: {chosenProduct.Condition}"); 
}

void ListProducts()
{
    decimal totalValue = 0.0M;
    foreach (Product product in products)
    {
        if (product.SoldOnDate == null)
        {
            totalValue += product.Price;
        }
    }
    Console.WriteLine($"Total inventory value: ${totalValue}");
    Console.WriteLine("Products:");
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {products[i].Name}");
    }
}


void ViewLatestProducts()
{
    // create a new empty List to store the latest products
    List<Product> latestProducts = new List<Product>();
    // Calculate a DateTime 90 days in the past
    DateTime threeMonthsAgo = DateTime.Now - TimeSpan.FromDays(90);
    //loop through the products
    foreach (Product product in products)
    {
        //Add a product to latestProducts if it fits the criteria
        if (product.StockDate > threeMonthsAgo && product.SoldOnDate == null) {
            latestProducts.Add(product);
        }
    }
    // print out the latest products to the console 
    for (int i = 0; i < latestProducts.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {latestProducts[i].Name}");
    }
}

void MonthlySalesReport()
{
    int year = 0;
    int month = 0;
    bool validYear = false;
    bool validMonth = false;
    while (!validYear)
    {
        try 
        {
            Console.WriteLine("Enter Year:");
            year = int.Parse(Console.ReadLine().Trim());
            validYear = true;
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
    }
    while (!validMonth)
    {
        try 
        {
            Console.WriteLine("Enter Month Number:");
            month = int.Parse(Console.ReadLine().Trim());
            if (month < 1 || month > 12)
            {
                Console.WriteLine("Month must be between 1 and 12.");
            }
            else
            {
                validMonth = true;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
    }
    List<Product> productsFound = products.Where(p => p.SoldOnDate != null && p.SoldOnDate.Value.Year == year && p.SoldOnDate.Value.Month == month).ToList();
    decimal totalMonthlySales = productsFound.Sum(p => p.Price);
    Console.WriteLine($"Sales Report for {year}, Month {month}:");
    Console.WriteLine($"Total Sales: ${totalMonthlySales:F2}");
}