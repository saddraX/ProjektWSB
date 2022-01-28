using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektWSB.Models
{
    public class AllList
    {
        public List<string> Dishes { get; set; }
        public List<string> Menus { get; set; }
        public List<string> Restaurants { get; set; }
        public List<string> Users { get; set; }
    }

    public class DishUserList
    {
        public List<Dish> Dishes { get; set; }
        public List<User> Users { get; set; }
    }
}