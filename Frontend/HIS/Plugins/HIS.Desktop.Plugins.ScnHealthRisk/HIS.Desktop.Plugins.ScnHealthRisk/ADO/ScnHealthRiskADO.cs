using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ScnHealthRisk.ScnHealthRisk.ADO
{
    class ScnHealthRiskADO : SCN.EFMODEL.DataModels.V_SCN_HEALTH_RISK
    {
        public bool isGetCheck { get; set; }
        public bool isQuitCheck { get; set; }

        public ScnHealthRiskADO() { }
        public ScnHealthRiskADO(SCN.EFMODEL.DataModels.V_SCN_HEALTH_RISK data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ScnHealthRiskADO>(this, data);
                    if (data.IS_GET == 1)
                    {
                        this.isGetCheck = true;
                    }
                    if (data.IS_QUIT == 1)
                    {
                        this.isQuitCheck = true;
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
