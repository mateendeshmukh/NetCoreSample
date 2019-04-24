using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstCore.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8,MinimumLength =5,ErrorMessage ="you must specify password betweem 5 to 8 character")]
        public string Password { get; set; }
    }

    public class UserForLoginDto
    {
     
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
