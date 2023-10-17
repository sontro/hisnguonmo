using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisDeliveryNoteList.ADO
{
    public class StatusAdo
    {
        public string Name { get; set; }
        public long id { get; set; }
        public StatusAdo(string _Name, long _id)
        {
            this.Name = _Name;
            this.id = _id;
        }
    }
}
