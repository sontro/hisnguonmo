using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RepayService.ADO
{
    public class SereServDepositADO : MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_DEPOSIT
    {
        public SereServDepositADO(MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_DEPOSIT _data)
        {
            try
            {
                if (_data != null)
                {

                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_DEPOSIT>();

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
