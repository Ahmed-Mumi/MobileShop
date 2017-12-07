using RS1_P120_MobitelShop.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS1_P120_MobitelShop.Models
{
    public class Login : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid { get; set; }
    }
}