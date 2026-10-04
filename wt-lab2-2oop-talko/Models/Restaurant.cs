using wt_lab2_2oop_talko.Models;

namespace wt_lab2_2oop_talko.Models;

public class Restaurant : BaseEntity
{
    public string Title { get; private set; }
    public string Address { get; private set; }

    public Restaurant(int id, string title, string address) : base(id)
    {
        Title = title;
        Address = address;
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"[Ресторан {Id}] {Title} | {Address}");
    }
}