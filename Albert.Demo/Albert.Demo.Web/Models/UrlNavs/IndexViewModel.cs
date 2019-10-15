using Albert.Demo.Domain.UrlNavs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Albert.Demo.Web.Models.UrlNavs
{
    public class IndexViewModel
    {
        public IReadOnlyList<UrlNav> UrlNavs { get; set; }
        public List<SelectListItem> Classifys { get; set; }

        public IndexViewModel(IReadOnlyList<UrlNav> urlNavs, List<SelectListItem> classifys)
        {
            UrlNavs = urlNavs;
            Classifys = classifys;
        }
    }
}
