using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            IsValid = true;
            StatusCode = HttpStatusCode.OK;
        }

        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
