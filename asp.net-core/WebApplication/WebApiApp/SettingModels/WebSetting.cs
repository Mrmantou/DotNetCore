using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApp.SettingModels
{
    public class WebSetting
    {
        public string WebName { get; set; }
        public string Title { get; set; }
        public Behavior Behavior { get; set; }
    }

    public class Behavior
    {
        public bool IsCheckIp { get; set; }
        public int MaxConnection { get; set; }
    }
}
