﻿using static System.Net.WebRequestMethods;

namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContentBio { get; set; }
        public string ImgSrc { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }

    }
}
