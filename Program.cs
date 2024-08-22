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
        SoldOnDate = new DateTime(2024, 7, 30, 14, 30, 0), 
        StockDate = new DateTime(2024, 7, 20, 9, 0, 0),
        ManufactureYear = 2021,
        Condition = 3.4
    },
    new Product()
    {
        Name = "Tennis Racket",
        Price = 45.00M,
        SoldOnDate = new DateTime(2024, 7, 30, 14, 45, 0), 
        StockDate = new DateTime(2024, 5, 5, 10, 0, 0),
        ManufactureYear = 2023,
        Condition = 4.0
    },
    new Product()
    {
        Name = "Baseball Glove",
        Price = 25.00M,
        SoldOnDate = new DateTime(2024, 8, 15, 18, 45, 0),
        StockDate = new DateTime(2024, 3, 15, 11, 0, 0),
        ManufactureYear = 2022,
        Condition = 3.9
    },
    new Product()
    {
        Name = "Golf Club",
        Price = 120.00M,
        SoldOnDate = new DateTime(2024, 8, 5, 9, 15, 0),
        StockDate = new DateTime(2023, 11, 10, 15, 0, 0),
        ManufactureYear = 2023,
        Condition = 4.7
    },
    new Product()
    {
        Name = "Soccer Ball",
        Price = 18.00M,
        SoldOnDate = new DateTime(2024, 8, 18, 12, 30, 0), 
        StockDate = new DateTime(2024, 2, 22, 8, 0, 0),
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
4. Monthly Sales Report
5. Add New Product
6. View Average Time in Stock
7. View Average Time in Stock for Sold Products
8. Display Busiest Sales Hours");
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
    else if (choice == "5")
    {
        AddProduct();
    }
    else if (choice == "6")
    {
        AverageTimeInStock(); 
    }
    else if (choice == "7")
    {
        AverageTimeInStockForSold(); 
    }
    else if (choice == "8")
    {
        BusiestSalesHours();
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
    
    UpdateProduct(chosenProduct);
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

void AddProduct()
{
    Console.WriteLine("\nAdd New Product");
    string name = "";
    decimal price = 0.0M;
    int yearMade = 0;
    double condition = 0.0;
    bool validName = false;
    bool validPrice = false;
    bool validYearMade = false;
    bool validCondition = false;
    while(!validName)
    {
        Console.WriteLine("Enter Name:");
        name = Console.ReadLine().Trim();
        if (!string.IsNullOrEmpty(name))
        {
            validName = true;
        }
        else
        {
            Console.WriteLine("Name cannot be empty.");
        }
    }
    while(!validPrice)
    {
        Console.WriteLine("Enter Price:");
        try
        {
            price = decimal.Parse(Console.ReadLine().Trim());
            if (price > 0)
            {
                validPrice = true;
            }
            else
            {
                Console.WriteLine("Price must be greater than 0.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid price format. Please enter a valid decimal number.");
        }
    }
    while(!validYearMade)
    {
        Console.WriteLine("Enter Manufacture Year:");
        try
        {
            yearMade = int.Parse(Console.ReadLine().Trim());
            int currentYear = DateTime.Now.Year;
            if (yearMade >= 1900 && yearMade <= currentYear)
            {
                validYearMade = true;
            }
            else
            {
                Console.WriteLine($"Year must be between 1900 and {currentYear}.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid year format. Please enter a valid integer.");
        }
    }
    while(!validCondition)
    {
        Console.WriteLine("Enter Condition (0.0 to 5.0):");
        try 
        {
            condition = double.Parse(Console.ReadLine().Trim());
            if (condition >= 0.0 && condition <= 5.0)
            {
                validCondition = true;
            }
            else
            {
                Console.WriteLine("Condition must be between 0.0 and 5.0.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid condition format. Please enter a valid number.");
        }
    }

    Product newProduct = new Product
    {
        Name = name,
        Price = price,
        SoldOnDate = null,
        StockDate = DateTime.Now,
        ManufactureYear = yearMade,
        Condition = condition
    };
    
    products.Add(newProduct);
    Console.WriteLine($@"Success! Your product has been added to the inventory.
Product Details:
    Name: {newProduct.Name}
    Price: {newProduct.Price:C} 
    Manufacture Year: {newProduct.ManufactureYear}
    Condition: {newProduct.Condition}
    Stock Date: {newProduct.StockDate:MMMM dd, yyyy}");
}

void UpdateProduct(Product chosenProduct)
{
   if (chosenProduct.SoldOnDate == null)
    {
       Console.WriteLine("Would you like to mark this product as sold? (y/n): ");
        string answer = Console.ReadLine().Trim().ToLower();
        if (answer == "y")
        {
            chosenProduct.SoldOnDate = DateTime.Now;
            Console.WriteLine($"The product '{chosenProduct.Name}' has been marked as sold on {chosenProduct.SoldOnDate:MMMM dd, yyyy}.");
        }
    } 
}

void AverageTimeInStock()
{
    List<Product> inStockProducts = products.Where(p => p.SoldOnDate == null).ToList();
    int sumDaysInStock = inStockProducts.Sum(p => p.TimeInStock.Days);
    int averageDays = sumDaysInStock / inStockProducts.Count;
    Console.WriteLine($"Average time in stock for currently available products: {averageDays} days.");
}

void AverageTimeInStockForSold()
{
    List<Product> soldProducts = products.Where(p => p.SoldOnDate != null).ToList();
    int sumDaysInStock = soldProducts.Sum(p => (p.SoldOnDate.Value - p.StockDate).Days);
    int averageDaysInStock = sumDaysInStock / soldProducts.Count;

    Console.WriteLine($"Average time in stock for sold products: {averageDaysInStock} days.");
}

void BusiestSalesHours()
{
    Dictionary<int, int> salesByHour = new Dictionary<int, int>();

    for (int i = 0; i < 24; i++)
    {
        salesByHour[i] = 0;
    }
    foreach (Product product in products)
    {
        if (product.SoldOnDate != null)
        {
            int hour = product.SoldOnDate.Value.Hour;
            salesByHour[hour]++;
        }
    }
    Console.WriteLine("Sales by hour:");
    for (int i = 0; i < 24; i++)
    {
        // Convert 24-hour format to 12-hour format and determine AM/PM
        int hour12 = i % 12;
        if (hour12 == 0) hour12 = 12; 
        string period = i < 12 ? "AM" : "PM";
        Console.WriteLine($"{hour12}:00-{hour12}:59{period} | {salesByHour[i]} sales");
    }
}