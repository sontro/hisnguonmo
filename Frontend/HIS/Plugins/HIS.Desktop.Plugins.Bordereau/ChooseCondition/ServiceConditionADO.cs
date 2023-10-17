using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Bordereau.ChooseCondition
{
    class ServiceConditionADO : HIS_SERVICE_CONDITION
    {
        public string HEIN_RATIO_STR { get; set; }
        public ServiceConditionADO()
        { }
        public ServiceConditionADO(HIS_SERVICE_CONDITION data)
        {
            try
            {
                System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<HIS_SERVICE_CONDITION>();
                foreach (var item in pi)
                {
                    item.SetValue(this, (item.GetValue(data)));
                }
                this.HEIN_RATIO_STR = (data.HEIN_RATIO * 100).ToString();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
