using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DepositService.ADO
{
    public class SereServByTreatmentADO : MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_5
    {
        public long? DEPOSIT_ID { get; set; }
        public long? REPAY_ID { get; set; }
        public SereServByTreatmentADO(MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_5 _data)
        {
            try
            {
                if (_data != null)
                {

                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_5>();

                    foreach (var item in pi)
                    {
                        item.SetValue(this, (item.GetValue(_data)));
                    }
                }
            }

            catch (Exception)
            {

            }
        }
    }
}
