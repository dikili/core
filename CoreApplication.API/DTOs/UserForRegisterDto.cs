using System;
using System.ComponentModel.DataAnnotations;

namespace CoreApplication.API.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        // [StringLength(8,MinimumLength=4,ErrorMessage="You must specify a password between 4 and 8 characters")]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
       
        public string Country { get; set; }
        [Required]
        public string l39 { get; set; }

          public string businessName { get; set; }
        public string inst { get; set; }
        public string twit { get; set; }
        public string face { get; set; }

        [Required]
        public string chessLevel { get; set; }
         public DateTime Created { get; set; }

         public DateTime LastActive { get; set; }

         public UserForRegisterDto()
         {
                Created=DateTime.Now;
                LastActive=DateTime.Now;
         }
    }
}