using Albert.Demo.Domain.UrlNavs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albert.Demo.Models.UrlNavs
{
    public class DeleteViewModel
    {
        public UrlNav UrlNav { get; set; }

        public string Token { get; set; }
    }
}
