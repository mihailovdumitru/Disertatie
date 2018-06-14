using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dto
{
    public class Token
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
