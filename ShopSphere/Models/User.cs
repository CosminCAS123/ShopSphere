using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models;


[PrimaryKey("UserID")]
    public class User
    {

       public int UserID { get; set; }
       
    public string? SecurityPIN { get; set; }

       public string? Username { get; set; }
       public string? PasswordHash { get; set; }
       public string? EmailAdress { get; set; }

       public string? FirstName { get; set; }

       public string? LastName { get; set;}

       public string? PhoneNumber { get; set; }

       public string? ProfilePictureUrl { get; set; }

       public int? Age { get; set; }

    public User()
    {
        
    }




}

