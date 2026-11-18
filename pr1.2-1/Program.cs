using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

// Завдання 2.1. Масив об'єктів Product: пошук, фільтрація, сортування, LINQ

public class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, string category, decimal price, int quantity)
    {
        Name = name;
        Category = category;
        Price = price;
        Quantity = quantity;
    }

    public decimal GetTotalValue() => Price * Quantity;

    public override string ToString()
    {
        return $"{Name} [{Category}]: {Price:F2} грн x {Quantity} = {GetTotalValue():F2} грн";
    }
}

class Program
{
    // Бульбашкове сортування масиву за ціною (за зростанням)
    static void BubbleSortByPrice(Product[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            bool swapped = false;
            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                if (array[j].Price > array[j + 1].Price)
                {
                    // Обмін сусідніх елементів
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    swapped = true;
                }
            }
            if (!swapped) break; // масив уже впорядкований
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        // TODO 1: масив з 8 продуктів трьох категорій
        Product[] products = new Product[]
        {
            new Product("Ноутбук",    "Електроніка", 32000m, 3),
            new Product("Смартфон",   "Електроніка", 18500m, 5),
            new Product("Навушники",  "Електроніка", 2300m,  10),
            new Product("Хліб",       "Продукти",    25.50m, 100),
            new Product("Молоко",     "Продукти",    42m,    60),
            new Product("Сир",        "Продукти",    210m,   15),
            new Product("Футболка",   "Одяг",        450m,   40),
            new Product("Куртка",     "Одяг",        3200m,  8)
        };

        // TODO 2: вивести всі продукти
        Console.WriteLine("=== Усі продукти ===");
        foreach (Product p in products)
            Console.WriteLine(p);

        // TODO 3: найдорожчий продукт (цикл foreach, без LINQ)
        Product mostExpensive = products[0];
        foreach (Product p in products)
        {
            if (p.Price > mostExpensive.Price)
                mostExpensive = p;
        }
        Console.WriteLine("\n=== Найдорожчий продукт ===");
        Console.WriteLine(mostExpensive);

        // TODO 4: найдешевший продукт
        Product cheapest = products[0];
        foreach (Product p in products)
        {
            if (p.Price < cheapest.Price)
                cheapest = p;
        }
        Console.WriteLine("\n=== Найдешевший продукт ===");
        Console.WriteLine(cheapest);

        // TODO 5: загальна вартість усіх продуктів
        decimal total = 0m;
        foreach (Product p in products)
            total += p.GetTotalValue();
        Console.WriteLine("\n=== Загальна вартість усіх продуктів ===");
        Console.WriteLine($"{total:F2} грн");

        // TODO 6: фільтрація за категорією (без LINQ)
        Console.WriteLine("\n=== Продукти категорії \"Електроніка\" ===");
        foreach (Product p in products)
        {
            if (p.Category == "Електроніка")
                Console.WriteLine(p);
        }

        // TODO 7: бульбашкове сортування за ціною (працюємо з копією масиву)
        Product[] sorted = (Product[])products.Clone();
        BubbleSortByPrice(sorted);
        Console.WriteLine("\n=== Сортування за ціною (бульбашкове) ===");
        foreach (Product p in sorted)
            Console.WriteLine(p);

        // TODO 8: List<Product> та LINQ
        List<Product> list = products.ToList();

        Console.WriteLine("\n=== LINQ ===");

        // Where — фільтрація
        var expensive = list.Where(p => p.Price > 1000m);
        Console.WriteLine("Where (ціна > 1000 грн):");
        foreach (Product p in expensive)
            Console.WriteLine("  " + p);

        // OrderBy — сортування за кількістю на складі
        var byQuantity = list.OrderBy(p => p.Quantity);
        Console.WriteLine("OrderBy (за кількістю, за зростанням):");
        foreach (Product p in byQuantity)
            Console.WriteLine("  " + p);

        // Average — середня ціна
        decimal avgAll = list.Average(p => p.Price);
        decimal avgElectronics = list.Where(p => p.Category == "Електроніка")
                                     .Average(p => p.Price);
        Console.WriteLine($"Середня ціна всіх продуктів: {avgAll:F2} грн");
        Console.WriteLine($"Середня ціна електроніки: {avgElectronics:F2} грн");
    }
}