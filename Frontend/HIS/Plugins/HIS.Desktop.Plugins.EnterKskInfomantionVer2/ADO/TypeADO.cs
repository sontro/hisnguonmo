using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EnterKskInfomantionVer2.ADO
{
    class TypeADO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TypeADO(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
