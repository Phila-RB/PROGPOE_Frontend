using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROGPOE_Frontend.Models
{
    public class User
    {
        //User model
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Access { get; set; }
        public string Password { get; set; }
    }
}
