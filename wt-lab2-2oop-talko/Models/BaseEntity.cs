using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wt_lab2_2oop_talko.Models;

   public abstract class BaseEntity
{
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected BaseEntity(int id)
        {
            Id = id;
            CreatedAt = DateTime.Now;
        }

        public abstract void PrintInfo();
    }

