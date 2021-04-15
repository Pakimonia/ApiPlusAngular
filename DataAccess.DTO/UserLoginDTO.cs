using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email is required field")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required field")]
        public string Password { get; set; }
    }
}
