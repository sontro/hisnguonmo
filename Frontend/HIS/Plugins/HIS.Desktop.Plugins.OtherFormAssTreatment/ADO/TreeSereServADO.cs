using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.OtherFormAssTreatment.ADO
{
    public class TreeSereServADO : HIS_SERE_SERV
    {
        public string CONCRETE_ID__IN_SETY { get; set; }
        public string PARENT_ID__IN_SETY { get; set; }
        public string SERVICE_REQ_CODE { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public long? NUM_ORDER { get; set; }

        public string VALUE_RANGE { get; set; }

        public decimal? VALUE { get; set; }

        public string DESCRIPTION { get; set; }

        public string TEST_INDEX_CODE { get; set; }

        public bool IsLeaf { get; set; }

        public bool? IS_NORMAL { get; set; }
        public bool? IS_LOWER { get; set; }
        public bool? IS_HIGHER { get; set; }

        public decimal? MIN_VALUE { get; set; }
        public decimal? MAX_VALUE { get; set; }

        public TreeSereServADO() { }

        public TreeSereServADO(HIS_SERE_SERV data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<TreeSereServADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
