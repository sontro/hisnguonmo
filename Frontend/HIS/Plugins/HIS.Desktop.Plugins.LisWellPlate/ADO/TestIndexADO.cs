using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.LisWellPlate.ADO
{
    class TestIndexADO : V_LIS_RESULT
    {
        public string TEST_INDEX_UNIT_NAME { get; set; }

        public TestIndexADO()
        {            
        }
        public TestIndexADO(V_LIS_RESULT data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<V_LIS_RESULT>(this, data);
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
