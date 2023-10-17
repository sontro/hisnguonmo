using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SampleCollectionRoom.ADO
{
    public class SampleListViewADO : V_LIS_SAMPLE
    {
        public SampleListViewADO()
        {

        }

        public SampleListViewADO(V_LIS_SAMPLE data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<SampleListViewADO>(this, data);
        }
        public bool IsChecked { get; set; }
    }
}
