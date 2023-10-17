using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Repay.ADO
{
    public class DereDetailADO : MOS.EFMODEL.DataModels.V_HIS_DERE_DETAIL
    {
        public DereDetailADO(MOS.EFMODEL.DataModels.V_HIS_DERE_DETAIL _data)
        {
            try
            {
                if (_data != null)
                {

                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.V_HIS_DERE_DETAIL>();

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
