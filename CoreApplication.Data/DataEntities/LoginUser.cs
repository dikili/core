using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace CoreApplication.Data.DataEntities
{
    public class LoginUser : IdentityUser<int>
    {


        public LoginUser()
        {
            Photos = new Collection<Photo>();
        }
        public string Gender { get; set; }

        public string l39 {get;set;}

           public string businessName {get;set;}

        
           public string businessPurpose {get;set;}

           public string busCategory { get; set; }  
           public string busExplain { get; set; }
         public string inst {get;set;}

        public string twit {get;set;} 
        public string face {get;set;} 
        public string chessLevel {get;set;}
        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Introduction { get; set; }       

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string  Country { get; set; }         

       public ICollection<Photo> Photos { get; set; }

       public ICollection<Like> Likee { get; set; } //one user can like many users

       public ICollection<Like> Liker { get; set; } //one user can have multiple users likes

        public ICollection<Message> MessagesSent { get; set; }

        public ICollection<Message> MessagesReceived { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }


    }
}