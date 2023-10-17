using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.ADO
{
    public class TimeADO : HisNumOrderBlockSDO
    {
        public string HOUR { get; set; }
        public string HOUR_STR { get; set; }

        public TimeADO(HisNumOrderBlockSDO data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<TimeADO>(this, data);
                    if (!String.IsNullOrWhiteSpace(data.FROM_TIME))
                    {
                        this.HOUR = data.FROM_TIME.Substring(0, 2);

                        this.HOUR_STR = Base.GlobalVariablesProcess.GenerateHour(data.FROM_TIME);
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
