using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMestInveUser
{
    public class HisMestInveUserAdo : HIS_MEST_INVE_USER
    {
        public int Action { get; set; }

        public HisMestInveUserAdo() { }
        public HisMestInveUserAdo(HIS_MEST_INVE_USER data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<HisMestInveUserAdo>(this, data);
            }
        }
        public HisMestInveUserAdo(HIS_USER_GROUP_TEMP_DT data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<HisMestInveUserAdo>(this, data);
            }
        }
    }
}
