using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApproveKskTreatment
{
    public class TreatmentADO : MOS.EFMODEL.DataModels.V_HIS_TREATMENT_4
    {
        public bool CheckTreatment { get; set; }

        public TreatmentADO(MOS.EFMODEL.DataModels.V_HIS_TREATMENT_4 treatment)
        {
            if (treatment != null)
            {
                System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.V_HIS_TREATMENT_4>();
                foreach (var item in pi)
                {
                    item.SetValue(this, (item.GetValue(treatment)));
                }
            }
        }
    }
}
