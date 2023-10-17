using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedAssign
{
    public class HisBedADO : MOS.EFMODEL.DataModels.V_HIS_BED
    {
        public string AMOUNT { get; set; }
        public long IsKey { get; set; }

        public HisBedADO() { }

        public HisBedADO(MOS.EFMODEL.DataModels.V_HIS_BED data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<HisBedADO>(this, data);
                    this.AMOUNT = 0 + "/" + this.MAX_CAPACITY;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
