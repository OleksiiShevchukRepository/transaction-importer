using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TransactionImporter.Rest.Attributes
{
    public class FormFileValidationAttribute : ActionFilterAttribute
    {
        private readonly long _maxFileSize;
        private readonly string[] _allowedExtensions;
        public FormFileValidationAttribute(long maxFileSize, string[] allowedExtensions)
        {
            _maxFileSize = maxFileSize;
            _allowedExtensions = allowedExtensions;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var formFile = context.HttpContext.Request.Form.Files.Any() ?
                context.HttpContext.Request.Form.Files[0] : null;

            if (!IsFileValid(formFile))
            {
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                var json = JsonConvert.SerializeObject(
                    new { Title = "Unknown file format", Status = HttpStatusCode.BadRequest });

                await context.HttpContext.Response.WriteAsync(json, Encoding.UTF8);
            }
            else
                await next();
        }

        private bool IsFileValid(IFormFile file)
        {
            if (file == null)
                return false;

            var fileExtension = Path.GetExtension(file.FileName);
            return !(file.Length > _maxFileSize || !_allowedExtensions.Contains(fileExtension.ToLower()));
        }
    }
}
