using Albert.Demo.Application.UrlNavs.Dtos;
using Albert.Demo.Domain.UrlNavs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Albert.Demo.Application.UrlNavs
{
    public interface IUrlNavAppService
    {
        Task<List<UrlNav>> GetUrlNavs(GetUrlNavArg input);

        Task Create(UrlNav  urlNav);
    }
}
