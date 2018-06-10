using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dto
{
    public class Token:User
    {
        public DateTime ExpiresAt { get; set; }
    }
}
