using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Services.Infrastructure
{
    public class FileGenerator:IFileGenerator
    {
        public ContentResult GenerateFile<T>(List<T> objects)
        {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            

            foreach(var objectElem in objects)
            {
                foreach(var property in properties)
                {
                    var value = property.GetValue(objectElem);
                    if (value != null)
                    {
                        sb.Append(value.ToString());
                    }
                    sb.Append("\t");
                }
                sb.Append(Environment.NewLine);
            }

            /*var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            result.Content = new StringContent(sb.ToString());*/

            var resp = new ContentResult
            {
                Content = sb.ToString(),
                StatusCode = 200,
                ContentType = "text/plain"
            };

            return resp;
        }
    }
}
