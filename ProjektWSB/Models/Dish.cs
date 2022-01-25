using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ProjektWSB.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }

        public Dish(int id, int userId, string name, string type, float price)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Type = type;
            Price = price;
        }
    }
}