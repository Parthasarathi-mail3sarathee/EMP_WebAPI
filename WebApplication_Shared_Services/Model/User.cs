﻿namespace WebApplication_Shared_Services.Model
{
    public class User
    {
        public string Name {get;set;}
        public int ID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
