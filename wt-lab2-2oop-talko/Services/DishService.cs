using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using wt_lab2_2oop_talko.Interfaces;
using wt_lab2_2oop_talko.Models;


namespace wt_lab2_2oop_talko.Services;

public class DishService : IItemService<Dish>
{
    private List<Dish> dishList = new List<Dish>();

    public void Add(Dish dish)
    {
        dishList.Add(dish);
    }

    //переопределение одним именем, но разными параметрами
    public void Add(int id, string name, decimal price, string category)
    {
        Dish d = new Dish(id, name, price, category);
        dishList.Add(d);
    }

    public Dish GetById(int id)
    {
        for (int i = 0; i < dishList.Count; i++)
        {
            if (dishList[i].Id == id)
            {
                return dishList[i];
            }
        }
        return null;
    }

    public List<Dish> GetAll()
    {
        return dishList;
    }

    public bool Remove(int id) {
        Dish d = GetById(id);
        if (d != null) {
            return false;
        } 
        dishList.Remove(d); 
        return true;
    }
}
