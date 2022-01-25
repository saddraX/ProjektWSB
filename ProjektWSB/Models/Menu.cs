using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektWSB.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int DishId { get; set; }

        public Menu(int id, int userId, int restaurantId, int dishId)
        {
            Id = id;
            UserId = userId;
            RestaurantId = restaurantId;
            DishId = dishId;
        }
    }
}