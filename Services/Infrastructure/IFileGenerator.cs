using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Services.Infrastructure
{
    public interface IFileGenerator
    {
        ContentResult GenerateFile<T>(List<T> objects);
    }
}