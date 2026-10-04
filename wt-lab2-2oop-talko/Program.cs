using wt_lab2_2oop_talko.Interfaces;
using wt_lab2_2oop_talko.Models;
using wt_lab2_2oop_talko.Services;


namespace wt_lab2_2oop_talko;


public class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var d1 = new Dish(1, "Пицца Маргарита ", 450m, "Пицца ");
        var d2 = new Dish(2, "Суши Филадельфия", 620m, "Суши  ");
        var d3 = new Dish(3, "Борщ украинский ", 320m, "Супы  ");
        var d4 = new Dish(4, "Цезарь с курицей", 410m, "Салаты");

        Console.WriteLine("Демонстрация БЛОК 1 / Уровень 1");
        d1.PrintInfo();
        d2.PrintInfo();
        d3.PrintInfo();
        d4.PrintInfo();
        Console.WriteLine();

        Console.WriteLine("Полиморфизм через BaseEntity[]");
        BaseEntity[] entities =
        {
    new Dish(1, "Пицца Маргарита", 450m, "Пицца"),
    new Courier(1, "Иванов Иван", "+375291234567"),
    new Restaurant(1, "Вкусно и точка", "г. Минск, ул. Ленина 1")
};

        foreach (var e in entities)
        {
            e.PrintInfo();
        }
        Console.WriteLine();

        Console.WriteLine("БЛОК 1 / Уровень 3 — полиморфизм через интерфейс");
       // интерфейсная ссылка
        IItemService<Dish> service = new DishService();

        //перегрузка
        service.Add(1, "Пицца Маргарита", 450m, "Пицца");
        Dish d = new Dish(2, "Суши Филадельфия", 599m, "Суши");
        service.Add(d);

        // методы ЧЕРЕЗ ИНТЕРФЕЙСНУЮ ССЫЛКУ
        List<Dish> all = service.GetAll();
        for (int i = 0; i < all.Count; i++)
        {
            all[i].PrintInfo();
        }

        Console.WriteLine();

        Console.WriteLine("БЛОК 2");

        //динам коллекция
        List<Dish> dishes = new List<Dish>();
        // 7 тестов
        dishes.Add(new Dish(1, "Пицца Маргарита", 450m, "Пицца"));
        dishes.Add(new Dish(2, "Суши Филадельфия", 599m, "Суши"));
        dishes.Add(new Dish(3, "Борщ украинский", 299m, "Супы"));
        dishes.Add(new Dish(4, "Цезарь с курицей", 410m, "Салаты"));
        dishes.Add(new Dish(5, "Пепперони", 520m, "Пицца"));
        dishes.Add(new Dish(6, "Том Ям", 640m, "Супы"));
        dishes.Add(new Dish(7, "Тирамису", 350m, "Десерты"));

        foreach (Dish dish in dishes)
        {
            dish.PrintInfo();
        }

        Console.WriteLine();

        //словарь
        Dictionary<int, Dish> dishById = new Dictionary<int, Dish>();
        foreach (Dish dish in dishes)
        {
            dishById.Add(dish.Id, dish);
        }
        Console.WriteLine("Словарь заполнен. Ключей: " + dishById.Count);
        Console.WriteLine();

        //trygetvalue
        int searchId1 = 3;
        Dish found1;
        bool ok1 = dishById.TryGetValue(searchId1, out found1); //out - нужно вернуть больше одного значения
        if (ok1)
        {
            Console.WriteLine("Блюдо с Id=" + searchId1 + " найдено:");
            found1.PrintInfo();
        }
        else
        {
            Console.WriteLine("Блюдо с Id=" + searchId1 + " не найдено.");
        }

        Console.WriteLine();

        // where
        Console.WriteLine("Where: только категория 'Пицца'");
        List<Dish> pizzas = dishes.Where(d => d.Category == "Пицца").ToList();
        foreach (Dish p in pizzas)
        {
            p.PrintInfo();
        }

        Console.WriteLine();

        //oderby по возрастанию
        Console.WriteLine("OrderBy: все блюда по возрастанию цены");
        List<Dish> sortedAsc = dishes.OrderBy(d => d.Price).ToList();
        foreach (Dish s in sortedAsc)
        {
            s.PrintInfo();
        }
        Console.WriteLine();

        //select
        Console.WriteLine("Select: только имена");
        List<string> names = dishes.Select(d => d.Name).ToList();
        foreach (string n in names)
        {
            Console.WriteLine(n);
        }
        Console.WriteLine();

        // LINQ
        Console.WriteLine("-- Count --");
        int total = dishes.Count();
        Console.WriteLine("Всего блюд: " + total);
        Console.WriteLine();

        Console.WriteLine("-- Sum --");
        decimal totalPrice = dishes.Sum(d => d.Price);
        Console.WriteLine("Сумма всех цен: " + totalPrice + " руб.");
        Console.WriteLine();

        Console.WriteLine("-- Average --");
        decimal avgPrice = dishes.Average(d => d.Price);
        Console.WriteLine("Средняя цена: " + avgPrice + " руб.");
        Console.WriteLine();

        Console.WriteLine("-- Min / Max --");
        decimal minPrice = dishes.Min(d => d.Price);
        decimal maxPrice = dishes.Max(d => d.Price);
        Console.WriteLine("Минимальная цена: " + minPrice + " руб.");
        Console.WriteLine("Максимальная цена: " + maxPrice + " руб.");
        Console.WriteLine();

        // GroupBy по категориям
        Console.WriteLine("-- GroupBy: блюда по категориям --");
        var groups = dishes.GroupBy(d => d.Category).ToList();

        foreach (var group in groups)
        {
            Console.WriteLine("Категория: " + group.Key);
            Console.WriteLine("  Количество блюд: " + group.Count());
            Console.WriteLine("  Средняя цена: " + group.Average(d => d.Price) + " руб.");
            Console.WriteLine("  Сумма цен: " + group.Sum(d => d.Price) + " руб.");
            Console.WriteLine();
        }
        Console.WriteLine();


        Console.WriteLine("-- Асинхронность --");
        await SimulateCookingAsync();
        Console.WriteLine();

        Console.WriteLine();

        List<Dish> loaded = await LoadDishesFromDbAsync();

        Console.WriteLine("Загружено блюд: " + loaded.Count);
        foreach (Dish dish in loaded)
        {
            dish.PrintInfo();
        }
        Console.WriteLine();

        // Task.WhenAll
        Console.WriteLine("=== Task.WhenAll ===");

        using CancellationTokenSource cts = new CancellationTokenSource();

        cts.CancelAfter(1000);

        Task<string> t1 = PrepareDishAsync("Пицца", cts.Token);
        Task<string> t2 = PrepareDishAsync("Суши", cts.Token);
        Task<string> t3 = PrepareDishAsync("Борщ", cts.Token);

        string[] results = await Task.WhenAll(t1, t2, t3); //ждём все сразу - итого 2 сек а не 6

        foreach (string r in results)
        {
            Console.WriteLine(r);
        }
        Console.WriteLine();

        PrintHeader();

       


        bool running = true;
        while (running)
        {
            PrintMenu();
            Console.Write("Ваш выбор: ");
            var input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("[пункт 1 будет реализован в БЛОКЕ 2]");
                    break;
                case "2":
                    Console.WriteLine("[пункт 2 будет реализован в БЛОКЕ 2]");
                    break;
                case "3":
                    Console.WriteLine("[пункт 3 будет реализован в БЛОКЕ 2]");
                    break;
                case "4":
                    Console.WriteLine("[пункт 4 будет реализован в БЛОКЕ 3]");
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("До свидания!");
                    break;
                default:
                    Console.WriteLine("Неверный пункт. Попробуйте снова.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню...");
                Console.ReadLine();
                Console.Clear();
                PrintHeader();
            }
        }
    }


    

    static void PrintHeader()
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║   СЛУЖБА ДОСТАВКИ ЕДЫ — Lab 2 (OOP)                  ║");
        Console.WriteLine("║   Вариант 9: Dish, Order, Courier, Restaurant        ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
    }

    static void PrintMenu()
    {
        Console.WriteLine("┌─────────────────── ГЛАВНОЕ МЕНЮ ───────────────────┐");
        Console.WriteLine("│  1. Показать все заказы                            │");
        Console.WriteLine("│  2. Показать меню блюд                             │");
        Console.WriteLine("│  3. Найти заказ по Id                              │");
        Console.WriteLine("│  4. Асинхронная доставка (async/await)             │");
        Console.WriteLine("│  0. Выход                                          │");
        Console.WriteLine("└────────────────────────────────────────────────────┘");
    }

    static async Task SimulateCookingAsync()
    {
        Console.WriteLine("Готовим заказ...");
        await Task.Delay(1000);
        Console.WriteLine("Заказ готов!");
    }

    static async Task<List<Dish>> LoadDishesFromDbAsync()
    {
        List<Dish> loaded = new List<Dish>();

        try //основной код
        {
            Console.WriteLine("Загрузка блюд из БД...");
            await Task.Delay(1500);

            // Имитация ошибки — раскоммен-уй, чтобы проверить catch:
            // throw new Exception("Ошибка подключения к БД");

            loaded.Add(new Dish(101, "Пицца 4 сыра", 550m, "Пицца"));
            loaded.Add(new Dish(102, "Рамен", 480m, "Супы"));
            loaded.Add(new Dish(103, "Чизкейк", 370m, "Десерты"));
        }
        catch (Exception ex) //выполняется, только если в try произошла ошибка
        {
            Console.WriteLine("Ошибка при загрузке: " + ex.Message);
        }
        finally //выполняется всегда 
        {
            Console.WriteLine("Загрузка завершена (finally).");
        }

        return loaded;
    }


    static async Task<string> PrepareDishAsync(string name, CancellationToken token)
    {
        try
        {
            Console.WriteLine("Готовим: " + name);
            await Task.Delay(2000, token); //без token пауза не прервётся, даже если придёт отмена
            return name + " готов";
        }
        catch (OperationCanceledException)
        {
            return name + " отменён";
        }
    }

}