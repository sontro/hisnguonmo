using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentPatientUpdate.Base
{
    public class HisPatientADO : MOS.EFMODEL.DataModels.V_HIS_PATIENT
    {
        public bool IsChecked { get; set; }

        public HisPatientADO()
        {
            IsChecked = false;
        }

        public HisPatientADO(MOS.EFMODEL.DataModels.V_HIS_PATIENT patient)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<HIS.Desktop.Plugins.TreatmentPatientUpdate.Base.HisPatientADO>(this, patient);
            IsChecked = false;
        }
    }
}
