﻿using Diplom.Models.Entity;


namespace Diplom.Models.Account
{
    public class User
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        public string Password { get; set; }
        public Role Role { get; set; }



        public Student Student { get; set; }

        public Professor Professor { get; set; }

    }
}