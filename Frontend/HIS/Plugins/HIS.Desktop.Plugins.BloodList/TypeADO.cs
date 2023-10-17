using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BloodList
{
    class TypeADO
    {
        public int ID { get; set; }
        public string TYPE_NAME { get; set; }
        
        public TypeADO()
        {

        }
        public TypeADO(int id, string name)
        {
            this.ID = id;
            this.TYPE_NAME = name;
        }
    }
    
}
