using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ScnPersonalHealth
{
    public class ScnSurgeryADO : SCN.EFMODEL.DataModels.SCN_SURGERY
    {
        public int Action { get; set; }
        public DateTime? Time { get; set; }

        public ScnSurgeryADO() { }

        public ScnSurgeryADO(SCN.EFMODEL.DataModels.SCN_SURGERY data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ScnSurgeryADO>(this, data);
                    if (data.SURGERY_TIME != null)
                    {
                        this.Time = (DateTime)Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.SURGERY_TIME ?? 0);
                    }
                    else
                    {
                        this.Time = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
