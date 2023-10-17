using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMachineServMaty.ADO
{
    public class ServiceADO : MOS.EFMODEL.DataModels.V_HIS_SERVICE
    {
        public bool IsHightLight { get; set; }

        public ServiceADO()
        { }

        public ServiceADO(MOS.EFMODEL.DataModels.V_HIS_SERVICE _data)
        {
            try
            {
                if (_data != null)
                {

                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.V_HIS_SERVICE>();

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
