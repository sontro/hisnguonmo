using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisKskDriverCreate.ADO
{
    class TypeADO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public TypeADO(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public TypeADO(long id, string code, string name)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
        }
    }
}
