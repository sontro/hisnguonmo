using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestViewDetail.ADO
{
    public class ExpMestBloodADODetail : V_HIS_EXP_MEST_BLOOD
    {
        public decimal AMOUNT { get; set; }

        public ExpMestBloodADODetail()
        {

        }

        public ExpMestBloodADODetail(V_HIS_EXP_MEST_BLOOD _data)
        {
            try
            {
                if (_data != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<V_HIS_EXP_MEST_BLOOD>();
                    foreach (var item in pi)
                    {
                        item.SetValue(this, (item.GetValue(_data)));
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
