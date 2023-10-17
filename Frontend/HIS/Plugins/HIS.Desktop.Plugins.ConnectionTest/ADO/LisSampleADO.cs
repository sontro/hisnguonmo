using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConnectionTest.ADO
{
    public class LisSampleADO : V_LIS_SAMPLE
    {
        public bool IsCheck { get; set; }

        public LisSampleADO(V_LIS_SAMPLE data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<LisSampleADO>(this, data);
            }
        }
    }
}
