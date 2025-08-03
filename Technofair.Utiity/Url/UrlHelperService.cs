using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Url
{

    public interface IUrlHelperService
    {
        string GetBaseUrl();
    }
    public class UrlHelperService: IUrlHelperService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlHelperService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return string.Empty;

            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }

}
