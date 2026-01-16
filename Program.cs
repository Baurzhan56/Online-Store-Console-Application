using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
namespace CsharpEssentials
{
    public class User
    {
        public string Name;
        public List<Product> ownOrders;
        public User(string name, List<Product> booked)
        {
            Name = name;
            ownOrders = booked;
        }
    }
    public class Order
    {
        public List<Product> Products;
        public decimal FullPrice;
        public Order(List<Product> products)
        {
            Products = products;
            foreach (var product in products)
            {
                FullPrice = product.Price;
            }
        }
    }
    public class Store
    {
        public List<Product> Products;
        public List<Product> Basket;
        public List<Order> Orders;
        public Store()
        {
            Products = new List<Product>
            {
            new Product("Хлеб", 25),
            new Product("Молоко", 100),
            new Product("Печенье", 50),
            new Product("Масло", 250),
            new Product("Йогурт", 300),
            new Product("Сок", 80),
            };
            Basket = new List<Product>();
            Orders = new List<Order>();
        }

        public void ShowCatalog()
        {
            Console.WriteLine("Каталог продуктов");
            ShowProducts(Products);

        }
        public void ShowBasket()
        {
            Console.WriteLine("Содержимое корзины.");
            ShowProducts(Basket);
        }
        public void ShowProducts(List<Product> Products)
        {
            int number = 1;
            foreach (var product in Products)
            {
                Console.Write(number + ". ");
                product.Print();
                number++;
            }
        }
        public void AddToBasket(int numberProduct)
        {
            Basket.Add(Products[numberProduct - 1]);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Продукт {Products[numberProduct - 1].Name} успешно добавлен.");
            Console.WriteLine($"В корзине {Basket.Count} продуктов.");
            Console.ResetColor();
        }
        public void CreateOrder()
        {
            //передать в доставку
            Order order = new Order(Basket);
            Orders.Add(order);
            
        }
        public void Questions()
        {
            Console.WriteLine("Если у вас возникли вопросы? Позвоните по номеру +7XXX-XXX-XXXX");
        }
    }
    public class Product
    {
        public string Name;
        public decimal Price;
        public void Print()
        {
            Console.WriteLine($"{Name} {Price}");
        }
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
    class Program
    {
        static void Main()
        {
            bool asking = true;
            int productNumber = 1;
            string IsNumber;
            Store onlineStore = new Store();
            while(asking)
            {
                ShowMenu();
                asking = false;
                IsNumber = Console.ReadLine();
                int numberAction = CheckInput(IsNumber);
                switch (numberAction)
                {
                    case 1: onlineStore.ShowCatalog();Console.ReadKey(); break;
                    case 2: onlineStore.ShowCatalog();
                    productNumber = Convert.ToInt32(Console.ReadLine());
                    onlineStore.AddToBasket(productNumber);
                    Console.ReadKey();break;
                    case 3: onlineStore.ShowBasket();
                    Console.ReadKey(); break;
                    case 4: Console.WriteLine("На чьё имя будет доставка? Напишите имя");
                    string nameOfAdress = Console.ReadLine();
                    User user = new User(nameOfAdress, onlineStore.Basket);
                    onlineStore.CreateOrder(); 
                    Console.WriteLine($"Заказ на имя {nameOfAdress} успешно создан и отправлено:");
                    onlineStore.ShowProducts(user.ownOrders);
                    onlineStore.Basket.Clear();
                    Console.ReadKey();break;
                    case 5: onlineStore.Questions(); 
                    Console.ReadKey();break;
                    case 6:
                    onlineStore.Products.Add(Admining());
                    Console.WriteLine("Товар добавлен успешно!");
                    Console.ReadKey();break;
                    default:
                        Console.ReadKey();
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("Повторите ввод");Console.ResetColor();
                        Console.WriteLine();break;
                }
                
                asking = true;
            }
            static Product Admining()
            {
                
                Console.WriteLine("Введите пароль админа:");
                string password = Console.ReadLine();
                if(password != "admin")
                {
                    Console.WriteLine("Пароль неверный.");
                    return null;
                }
                FunctionAdmin();
                int i = Convert.ToInt32(Console.ReadLine());
                if(i == 1)
                {
                    Console.WriteLine("Напишите наименование товара");
                   string name = Console.ReadLine();
                   Console.WriteLine("Напишите цену товара");
                   decimal price = Convert.ToInt32(Console.ReadLine());
                   Product product = new Product(name, price); 
                   return product;
                }
                else 
                {
                    return null;
                }
                
            }
            static void FunctionAdmin()
            {
                Console.WriteLine("1. Добавить новый продукт в каталог.");
                
            }


        }
        static void ShowMenu()
        {
            Console.Write("Здравствуйте.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Выберие действие:");
            Console.ResetColor();
            Console.WriteLine("1.Показать каталог продукта?\n2.Хотите добавить продукт в корзину? \n3.Показать вашу корзину?\n4.Хотите оформить заказ? \n5.Возник вопрос? \n6.Зайти как администратор?");
            Console.WriteLine("Выберите номер действия, которое хотите совершить.");
        }
        static int CheckInput(string input)
        {
            if(char.IsDigit(input[0]))
            {
                if(Convert.ToInt32(input)>6)
                {
                    Console.WriteLine("Введена цифра выше из предложенных. Пожалуйста введите цифру из предложенных.");
                    return 7;
                }
                else if(Convert.ToInt32(input)<1)
                {
                    Console.WriteLine("Введена цифра ниже предложенных. Пожалуйста введите цифру из предложенных.");
                    return 7;
                }
                else
                {
                    return Convert.ToInt32(input);
                }
            }
            else
            {
                Console.WriteLine("Вы ввели не цифру.");
                return 7;
            }
        }
    }
}