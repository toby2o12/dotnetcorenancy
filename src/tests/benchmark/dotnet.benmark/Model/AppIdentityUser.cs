using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnet.benmark.Model
{
    public class AppIdentityUser : IdentityUser
    {
    }
    [Table("Sequence")]
    public class Sequence
    {
        [Key]
        public int Id { get; set; }
        //[Timestamp]
        //public int t { get; set; }
    }
    [Table("users")]
    public class Users
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }

}
