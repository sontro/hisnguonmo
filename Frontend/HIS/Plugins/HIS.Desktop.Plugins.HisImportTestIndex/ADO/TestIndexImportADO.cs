using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HIS.Desktop.Plugins.HisImportTestIndex.ADO
{
    public class TestIndexImportADO : V_HIS_TEST_INDEX
    {
        public string NUM_ORDER_STR { get; set; }
        public string ERROR { get; set; }

        public TestIndexImportADO()
        {
        }

        public TestIndexImportADO(V_HIS_TEST_INDEX data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<TestIndexImportADO>(this, data);
        }
    }
}
