using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HIS.Desktop.Plugins.HisImportTestIndexMap.ADO
{
    public class TestIndexMapImportADO : V_LIS_TEST_INDEX_MAP
    {
        public string ERROR { get; set; }

        public TestIndexMapImportADO()
        {
        }

        public TestIndexMapImportADO(V_LIS_TEST_INDEX_MAP data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<TestIndexMapImportADO>(this, data);
        }
    }
}
