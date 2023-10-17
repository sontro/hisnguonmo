using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SarUserReportTypeList
{
    public class ConfigAppADO : SDA.EFMODEL.DataModels.SDA_CONFIG_APP_USER
    {
        public string KEY { get; set; }

        public ConfigAppADO()
        {

        }

        public ConfigAppADO(SDA.EFMODEL.DataModels.SDA_CONFIG_APP configApp)
        {
            try
            {
                this.KEY = configApp.KEY;
                this.VALUE = configApp.DEFAULT_VALUE;
                this.CONFIG_APP_ID = configApp.ID;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public ConfigAppADO(SDA.EFMODEL.DataModels.SDA_CONFIG_APP_USER data, List<SDA.EFMODEL.DataModels.SDA_CONFIG_APP> configApps)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ConfigAppADO>(this, data);
                var confApp = (configApps != null ? configApps.FirstOrDefault(o => o.ID == data.CONFIG_APP_ID) : new SDA.EFMODEL.DataModels.SDA_CONFIG_APP());
                if (confApp == null) throw new ArgumentNullException("Get confApp by id is null. id = " + data.CONFIG_APP_ID);

                this.KEY = confApp.KEY;
                this.VALUE = (data != null ? (String.IsNullOrEmpty(data.VALUE) ? data.VALUE : confApp.DEFAULT_VALUE) : "").ToString();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
