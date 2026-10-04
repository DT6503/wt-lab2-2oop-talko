namespace wt_lab2_2oop_talko.Models
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; private set; }
        public string Category { get; set; }

        public Dish(int id, string name, decimal price, string category) : base(id)
        {
            Name = name;
            Category = category;
            UpdatePrice(price);
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"[Блюдо {Id}]: {Name} | {Category} | {Price} руб.");
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                Console.WriteLine("Цена должна быть положительной");
            }

            else
            {
                Price = newPrice;
            }
        }

        public void Rename(int newId)
        {
            Id = newId;       // ← ПИШЕМ Id. Внутри Dish. Разрешено — Dish наследник
        }
    }
}
