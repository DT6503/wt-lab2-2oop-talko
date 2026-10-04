using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wt_lab2_2oop_talko.Interfaces
{
    public interface IItemService<T>
    {
        void Add(T item);
        void Add(int id, string name, decimal price, string category);
        T GetById(int id);
        List<T> GetAll();
        bool Remove(int id);
    }
}
