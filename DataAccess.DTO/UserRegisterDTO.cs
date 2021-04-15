using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Email is required field")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required field")]
        public string Password { get; set; }

        [Required(ErrorMessage = "FullName is required field")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phone is required field")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required field")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Age is required field")]
        [Range(15, 100, ErrorMessage = "Age must be from 15 to 100")]
        public int  Age { get; set; }
    }
}
