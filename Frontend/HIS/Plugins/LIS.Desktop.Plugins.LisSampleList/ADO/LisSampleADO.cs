using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisSampleList.ADO
{
    class LisSampleADO : V_LIS_SAMPLE_1
    {
        public string SERVICE_NAMES { get; set; }

        public LisSampleADO(V_LIS_SAMPLE_1 data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<LisSampleADO>(this, data);
            }
        }
    }
}
