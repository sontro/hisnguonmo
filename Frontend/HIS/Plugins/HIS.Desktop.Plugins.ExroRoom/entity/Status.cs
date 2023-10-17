using System;
using System.Collections.Generic;
using System.Linq;

namespace HIS.Desktop.Plugins.ExroRoom.Entity
{
    public class Status
    {
        public long id { get; set; }
        public string statusName { get; set; }

        public Status(long id, string statusName)
        {
            this.id = id;
            this.statusName = statusName;
        }
    }
}
