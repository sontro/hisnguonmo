using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood.ADO
{
    public class VHisBloodPatyADO : V_HIS_BLOOD_PATY
    {
        public decimal ExpVatRatio { get; set; }
        public bool IsNotSell { get; set; }
        public bool IsNotEdit { get; set; }
        public long ServiceId { get; set; }
        public long ServiceTypeId { get; set; }

        public bool IsSetExpPrice;

        public VHisBloodPatyADO() { }

        public void SetValueByHisBloodPaty(HIS_BLOOD_PATY bloodPaty)
        {
            try
            {
                if (bloodPaty != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<HIS_BLOOD_PATY>();
                    foreach (var item in pi)
                    {
                        item.SetValue(this, item.GetValue(bloodPaty));
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
