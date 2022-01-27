using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjektWSB.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}