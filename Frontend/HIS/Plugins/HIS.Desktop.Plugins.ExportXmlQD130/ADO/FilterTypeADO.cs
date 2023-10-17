using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXmlQD130.ADO
{
    public class FilterTypeADO
    {
        public int id { get; set; }
        public string Name { get; set; }

        public FilterTypeADO(int _id, string _Name)
        {
            this.id = _id;
            this.Name = _Name;
        }
    }
}
