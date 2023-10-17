using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisPrescriptionList.Base
{
    public class PrescriptionADO : MOS.EFMODEL.DataModels.V_HIS_PRESCRIPTION_1
    {
        public string TREATMENT_CODE { get; set; }

        public PrescriptionADO() { }

        public PrescriptionADO(MOS.EFMODEL.DataModels.V_HIS_PRESCRIPTION_1 data)
        {
            try
            {
                if (data != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<V_HIS_PRESCRIPTION_1>();
                    foreach (var item in pi)
                    {
                        item.SetValue(this, item.GetValue(data));
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
