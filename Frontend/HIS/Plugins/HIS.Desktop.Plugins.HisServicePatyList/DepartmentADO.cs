using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServicePatyList
{
    public class DepartmentADO : HIS_DEPARTMENT
    {
        public bool Check { get; set; }

        public DepartmentADO()
        {
        }

        public DepartmentADO(HIS_DEPARTMENT departMent)
        {
            if (departMent != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<DepartmentADO>(this, departMent);
            }
        }
        public DepartmentADO(HIS_DEPARTMENT departMent, List<long> departmentIds)
        {
            if (departMent != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<DepartmentADO>(this, departMent);
                if (departmentIds.Contains(departMent.ID))
                    this.Check = true;
            }
        }
    }
}
