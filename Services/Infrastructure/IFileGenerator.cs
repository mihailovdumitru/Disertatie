using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Services.Infrastructure
{
    public interface IFileGenerator
    {
        ContentResult GenerateFile<T>(List<T> objects);
    }
}
