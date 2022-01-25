using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektWSB.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }

        public Restaurant(int id, int userId, string name, string address, long phoneNumber)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}