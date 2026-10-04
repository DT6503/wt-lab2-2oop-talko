using wt_lab2_2oop_talko.Models;

namespace wt_lab2_2oop_talko.Models;

public class Courier : BaseEntity
{
    public string FullName { get; private set; }
    public string Phone { get; private set; }

    public Courier(int id, string fullName, string phone) : base(id)
    {
        FullName = fullName;
        Phone = phone;
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"[Курьер {Id}] {FullName} | тел. {Phone}");
    }
}