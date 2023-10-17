using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedRoomPartial.ADO
{
    class DepartmentFilterADO
    {
        public long ID { get; set; }

        public string DepartmentFilter { get; set; }

        public DepartmentFilterADO(long id, string filterDepartment)
        {
            this.ID = id;
            this.DepartmentFilter = filterDepartment;
        }
    }
}
