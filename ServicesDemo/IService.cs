using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDemo
{
    public interface IService
    {
        Task<int> AddQuestion(string question);
    }
}
