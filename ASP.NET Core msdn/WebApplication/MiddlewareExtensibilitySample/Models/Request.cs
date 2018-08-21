using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExtensibilitySample.Models
{
    public class Request
    {
        public int Id { get; set; }
        public DateTime DT { get; set; }
        public string MiddlewareActivation { get; set; }
        public string Value { get; set; }
    }
}
