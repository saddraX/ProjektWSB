using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektWSB.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int DishId { get; set; }

        [NotMapped]
        public Dish Dish { get; set; }
    }
}