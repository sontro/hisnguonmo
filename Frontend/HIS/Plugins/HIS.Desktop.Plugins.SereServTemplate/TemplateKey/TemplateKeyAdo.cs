using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SereServTemplate.TemplateKey
{
    class TemplateKeyAdo
    {
        public string KEY { get; set; }
        public string VALUE { get; set; }

        public TemplateKeyAdo(string key, string value)
        {
            this.KEY = key;
            this.VALUE = value;
        }
    }
}
