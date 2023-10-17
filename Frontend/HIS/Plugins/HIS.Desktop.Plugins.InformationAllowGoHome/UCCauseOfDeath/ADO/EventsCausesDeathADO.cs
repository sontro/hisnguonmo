using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.ADO
{
    public class EventsCausesDeathADO : HIS_EVENTS_CAUSES_DEATH
    {
        public int actionType { get; set; }
        public string ICD_NAME { get; set; }
        public DateTime? Date { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public EventsCausesDeathADO() { }
        public EventsCausesDeathADO(HIS_EVENTS_CAUSES_DEATH data) {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<EventsCausesDeathADO>(this, data);
                this.actionType = HIS.Desktop.LocalStorage.LocalData.GlobalVariables.ActionEdit;
                this.Date = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.HAPPEN_TIME ?? 0);
                this.SERVICE_UNIT_NAME = data.UNIT_NAME;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
